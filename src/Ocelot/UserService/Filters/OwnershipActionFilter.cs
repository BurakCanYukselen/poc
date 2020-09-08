using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserService.Extensions;

namespace UserService.Filters
{
    public class OwnershipActionFilter : Attribute, IActionFilter
    {
        public string RouteKey { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var children = context.HttpContext.GetUserChildren();
            if (context.RouteData.Values.TryGetValue("childId", out var childId))
            {
                if (!children.Contains((string) childId))
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}