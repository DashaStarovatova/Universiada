using Infrastructure;
using Application;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;


// cd C:\Users\Darya\Desktop\Работа\Универсиада\Универсиада\src\API
// dotnet add package Swashbuckle.AspNetCore // установка swagger для тестов
// cd C:\Users\Darya\Desktop\Работа\Универсиада\Универсиада
// dotnet ef migrations add InitialPostgreSQL --project .\Infrastructure --startup-project .\API
// dotnet ef database update --project .\Infrastructure --startup-project .\API
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
// dotnet add package Microsoft.OpenApi
// dotnet add package Swashbuckle.AspNetCore --version 7.0.0

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем контроллеры
builder.Services.AddControllers();

// 2. Минимальная настройка Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT токен"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// 3. Регистрации
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 4. Доп. регистрации сервисов
builder.Services.AddScoped<IIdentityService, RealIdentityService>();

builder.Services.AddHttpContextAccessor(); 
builder.Services.AddScoped<IGetFilePath, GetFilePath>();

// 5. БД контекст
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new NotImplementedException());
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/universiada-realm";
        options.RequireHttpsMetadata = false; // для разработки

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,  // ← отключаем проверку аудитории
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8080/realms/universiada-realm"
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 6. Включаем Swagger
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.Map("/", () => Results.Redirect("/swagger"))
            .ExcludeFromDescription();

app.MapControllers();

app.MapGet("/debug-token", (HttpContext context) =>
{
    var user = context.User;
    if (user.Identity?.IsAuthenticated != true)
        return Results.Unauthorized();  // ← исправлено
    
    var claims = user.Claims.Select(c => $"{c.Type}: {c.Value}");
    return Results.Ok(new
    {
        IsAuthenticated = true,
        Claims = claims
    });
}).RequireAuthorization();

app.Run();
