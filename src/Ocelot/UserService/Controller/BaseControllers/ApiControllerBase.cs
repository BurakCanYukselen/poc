using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserService.Extensions;

namespace UserService.Controller.BaseControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected List<string> UserChildrens => HttpContext.GetUserChildren();
        protected List<string> UserRoles => HttpContext.GetUserRoles();
        protected List<string> UserPermissons => HttpContext.GetUserPermissions();
    }
}