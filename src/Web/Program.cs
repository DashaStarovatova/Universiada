using System.Reflection;
using Application;
using Application.Services;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Web.Components;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "universiada_cookie";
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
})
.AddOpenIdConnect(options =>
{
    options.Authority = "http://localhost:8080/realms/universiada-realm";
    options.ClientId = "web";
    options.ClientSecret = "VVXnoTTliSEx7T2WFLG74xUx5wU6t4Xp";
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.RequireHttpsMetadata = false;
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IEventCalendarService, StubEventCalendarService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IAnswersRepository, AnswersRepository>();
builder.Services.AddScoped<IResultsFactory, ResultsFacroty>();
builder.Services.AddSingleton<IAnswersResolver, MatlabResolver>();
builder.Services.AddScoped<IFileStore, FileStore>();

builder.Services.AddHostedService<MatlabWorker>();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection")))
);
// Добавление сервера Hangfire для выполнения фоновых задач
builder.Services.AddHangfireServer();

builder.Services.AddScoped<CheckAnswersService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.UseHangfireDashboard("/hangfire"); // Панель будет доступна по адресу /hangfire

RecurringJob.AddOrUpdate<CheckAnswersService>(
    "check-all-dates",
    service => service.RunCheck(CancellationToken.None),
    "30 18 * * *",
    new RecurringJobOptions
    {
        TimeZone = TimeZoneInfo.Local
    });

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();



app.Run();
