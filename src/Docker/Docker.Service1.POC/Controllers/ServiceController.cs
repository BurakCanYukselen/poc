using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Docker.Service1.POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        
        
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            string externalService;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://host.docker.internal:5001");
                // client.BaseAddress = new Uri("http://localhost:5001");
                var response = await client.GetStringAsync("service/get");
                
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(response);
                Console.WriteLine(Environment.NewLine);
                
                dynamic responseResult = JObject.Parse(response);
                externalService = responseResult.service;
            }

            var result = new
            {
                Service = AppDomain.CurrentDomain.FriendlyName,
                ExternalService = externalService,
            };

            return Ok(result);
        }
    }
}