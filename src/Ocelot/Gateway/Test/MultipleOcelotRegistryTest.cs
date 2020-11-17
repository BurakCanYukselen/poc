using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gateway.Extensions.ConfigurationBuilderExtension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Gateway.Test
{
    public class MultipleOcelotRegistryTest
    {
        [Fact]
        public async void Test()
        {
            var defautlPath = @"C:\Development\BCY\POC\src\Ocelot\Gateway\bin\Debug\netcoreapp3.1\Services";
            var auth = @"C:\Development\BCY\POC\src\Ocelot\Gateway\bin\Debug\netcoreapp3.1\Services\Auth\ocelot.auth.json";
            var user = @"C:\Development\BCY\POC\src\Ocelot\Gateway\bin\Debug\netcoreapp3.1\Services\User\ocelot.user.json";

            var authContent = await GetJsonContentFromFile(auth);
            var userContent = await GetJsonContentFromFile(user);

            var dynamicAuth = GetObjectFromJson(authContent);
            var dynamicUser = GetObjectFromJson(userContent);

            var routeAuth = dynamicAuth.Routes.First;
            var routeUser = dynamicUser.Routes.First;

            var mergedRoutes = new List<dynamic>();
            mergedRoutes.Add(routeAuth);
            mergedRoutes.Add(routeUser);

            var mergedObject = new {Route = mergedRoutes};
            var mergedJson = JsonConvert.SerializeObject(mergedObject);
            
            File.WriteAllText(Path.Combine(defautlPath,"ocelot.json"), mergedJson);

            var i = 1;
        }

        public async Task<string> GetJsonContentFromFile(string filePath)
        {
            var fileContent = await File.ReadAllTextAsync(filePath);
            return fileContent;
        }

        public dynamic GetObjectFromJson(string json)
        {
            dynamic content = JsonConvert.DeserializeObject(json);
            return content;
        }
    }
}