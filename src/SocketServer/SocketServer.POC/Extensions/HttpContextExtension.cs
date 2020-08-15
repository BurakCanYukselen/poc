using System;
using Microsoft.AspNetCore.Http;

namespace SocketServer.POC.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetFromQuery(this HttpContext context, string key)
        {
            context.Request.Query.TryGetValue(key, out var value);
            return value;
        }
    }
}