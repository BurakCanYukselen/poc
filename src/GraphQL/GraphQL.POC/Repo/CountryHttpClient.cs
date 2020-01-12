using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphQL.POC.Repo
{
    public interface ICountryHttpClient
    {
        Task<List<RestCountriesResponseModel>> GetAll();
    }

    public class CountryHttpClient : ICountryHttpClient
    {
        private readonly HttpClient _client;

        public CountryHttpClient(HttpClient client)
        {
            this._client = client;
        }

        public async Task<List<RestCountriesResponseModel>> GetAll()
        {
            var response = await _client.GetAsync("rest/v2/all");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<RestCountriesResponseModel>>(responseString);
            return result;
        }
    }
}
