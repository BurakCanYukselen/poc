using GraphQL.POC.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GraphQL.POC.Controllers
{
    public class CountryController : ApiController
    {
        private readonly ICountryHttpClient _countryHttpClient;

        public CountryController(ICountryHttpClient countryHttpClient)
        {
            this._countryHttpClient = countryHttpClient;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _countryHttpClient.GetAll();
            return Ok(result);
        }
    }
}
