// cd C:\Users\Darya\Desktop\Работа\Универсиада\Универсиада\Infrastructure
// dotnet add package Microsoft.Extensions.Configuration.Abstractions
// dotnet add package Microsoft.EntityFrameworkCore.InMemory
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;           // интерфейсы репозиториев
using Application.Interfaces;      // интерфейсы сервисов
using Infrastructure.Repositories;  // реализации репозиториев
using Infrastructure.Services;      // реализации сервисов
using Infrastructure.Data;          // AppDbContext

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрация db контекста
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Регистрация репозиториев
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<ILoadedFileRepository, LoadedFileRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        // Регистрация сервисов
        services.AddScoped<IMatlabRunner, MatlabRunner>();
        services.AddScoped<IResultsToCsv, ResultsToCsv>();
        services.AddScoped<IUploadFile, UploadFile>();

        return services;
    }
}
