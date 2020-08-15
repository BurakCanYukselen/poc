using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SocketServer.POC.Middlewares;
using SocketServer.POC.Sockets;

namespace SocketServer.POC.Extensions
{
    public static class SocketExtension
    {
        public static IApplicationBuilder MapSocketHandler<TSocketHandler>(this IApplicationBuilder app, string path)
        {
            app.MapWhen(context =>
            {
                var requestPath = context.Request.GetEncodedUrl();
                return requestPath.Contains(path);
            }, app => { app.UseMiddleware<SocketMiddleware>(Startup.GetService<TSocketHandler>()); });

            return app;
        }
    }
}