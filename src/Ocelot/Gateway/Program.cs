using Gateway.Extensions.ConfigurationBuilderExtension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hosting, config) =>
                    {
                        config.RegisterMultipleOcelotConfig("Services", hosting.HostingEnvironment);
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(cfg =>
                    {
                        cfg.Limits.MaxRequestHeadersTotalSize = int.MaxValue;
                    });
                });
    }
}