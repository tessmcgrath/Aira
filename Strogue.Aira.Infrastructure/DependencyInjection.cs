namespace Strogue.Aira.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //services.AddSingleton<IAzureServiceBus, AzureServiceBus>();
        return services;
    }
}