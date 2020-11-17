using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Docker.Service2.POC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new
            {
                Service = AppDomain.CurrentDomain.FriendlyName,
            };

            return Ok(result);
        }
    }
}