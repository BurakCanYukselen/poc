using Logger.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logger
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;

        public static void Init()
        {
            var services = new ServiceCollection()
                .AddScoped<DatabaseLogger>()
                .AddScoped<ConsoleLogger>()
                .AddScoped<IRepository, Repository>()
                .AddSingleton<IAppConfig, AppConfig>()
                .AddSingleton<ILocalDBConnection>(p => new LocalDBConnection("Data Source = (LocalDb)\\MSSQLLocalDB; Initial Catalog = MFLogPOC; Integrated Security = True"));

            serviceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public static T GetRequiredService<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
