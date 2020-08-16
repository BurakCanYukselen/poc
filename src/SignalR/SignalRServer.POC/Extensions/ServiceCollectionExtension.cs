using Microsoft.Extensions.DependencyInjection;

namespace SignalRServer.POC.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSignalRManager<THubManager, TConnection>(this IServiceCollection services)
            where THubManager : class
            where TConnection : class
        {
            services.AddSingleton<THubManager>();
            services.AddSingleton<TConnection>();
            return services;
        }
    }
}