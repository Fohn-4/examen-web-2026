using Core.IGateways;
using Core.UseCases;
using Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IUserUseCases, UserUseCases>();
        services.AddTransient<IEventUseCases, EventUseCases>();
        return services;
    }
}