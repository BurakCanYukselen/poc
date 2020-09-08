using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {

        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            if (username != "user" && password != "1")
                return BadRequest();
            
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("application_secret_key");
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim("userid", "360d4ca2-6c91-4288-8c09-285994965e99"),
                    new Claim("child","0094976D-8E6D-4B3B-9615-A1330CD1C3AF"),
                    new Claim("child","9DA0CF66-0B9C-4004-8C00-527179599EBE"),
                    new Claim("userRole", "owner"),
                    new Claim("userRole", "customer"),
                    new Claim("permission", "Owner_Read"),
                    new Claim("permission", "Owner_Write"),
                    new Claim("permission", "Customer"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = handler.CreateToken(descriptor);
            var userToken = handler.WriteToken(token);

            return Ok(userToken);
        }
    }
}