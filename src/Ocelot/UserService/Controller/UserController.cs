using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Controller.BaseControllers;
using UserService.Filters;

namespace UserService.Controller
{
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        [Route("{userid}/test-user")]
        public async Task<IActionResult> TestUser(string userid)
        {
            return Ok(new
            {
                userid,
                children = this.UserChildrens,
                userRoles = this.UserRoles,
                userPermissions = this.UserPermissons,
            });
        }
        
        [HttpGet]
        [Route("{userid}/{childId}/test-user-ownership")]
        [OwnershipActionFilter(RouteKey = "childId")]
        public async Task<IActionResult> TestUserOwnership(string childId, string userid)
        {
            return Ok(new
            {
                userid,
                children = this.UserChildrens,
                userRoles = this.UserRoles,
                userPermissions = this.UserPermissons,
            });
        }
    }
}