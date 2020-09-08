using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace UserService.Extensions
{
    public static class HttpContextExtensions
    {
        public static List<string> GetUserChildren(this HttpContext context) => context.GetHeaderValueFromKey("UserChildren").ToList();
        public static List<string> GetUserRoles(this HttpContext context) => context.GetHeaderValueFromKey("UserRoles").ToList();
        public static List<string> GetUserPermissions(this HttpContext context) => context.GetHeaderValueFromKey("UserPermissions").ToList();
        
        private static IEnumerable<string> GetHeaderValueFromKey(this HttpContext context, string key)
        {
            var values = context.Request.Headers.Where(p => p.Key == key).SelectMany(p => p.Value.ToString().Split(","));
            return values;
        }
    }
}