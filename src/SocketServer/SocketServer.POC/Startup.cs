using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocketServer.POC.Extensions;
using SocketServer.POC.Middlewares;
using SocketServer.POC.Sockets;

namespace SocketServer.POC
{
    public class Startup
    {
        private static IServiceProvider _serviceProvider;
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPrivateSocketManager, PrivateSocketManager>();
            services.AddSingleton<IPrivateSocketHandler, PrivateSocketHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            app.UseWebSockets();
            app.MapSocketHandler<IPrivateSocketHandler>("/ws/order");
        }

        public static TService GetService<TService>()
        {
            return _serviceProvider.GetService<TService>();
        } 
    }
}