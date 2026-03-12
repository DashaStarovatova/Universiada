using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
          this IServiceCollection services)
    {
        // Регистрация MediatR (все команды и запросы)
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;

    }
}