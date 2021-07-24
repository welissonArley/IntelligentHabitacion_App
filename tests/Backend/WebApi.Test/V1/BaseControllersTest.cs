using Homuai.Api;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test.V1
{
    public class BaseControllersTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private int ApiVersion => 1;
        private readonly HttpClient _httpClient;

        public BaseControllersTest(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        protected async Task<HttpResponseMessage> DoGetRequest(string method, string token = null)
        {
            AuthorizeRequest(token);

            return await _httpClient.GetAsync($"/api/v{ApiVersion}/{method}");
        }
        protected async Task<HttpResponseMessage> DoPostRequest(string method, object body, string token = null)
        {
            AuthorizeRequest(token);

            return await _httpClient.PostAsync($"/api/v{ApiVersion}/{method}", CreateContent(body));
        }

        protected async Task<HttpResponseMessage> DoPutRequest(string method, object body, string token = null)
        {
            AuthorizeRequest(token);

            return await _httpClient.PutAsync($"/api/v{ApiVersion}/{method}", CreateContent(body));
        }

        private StringContent CreateContent(object body)
        {
            if (body == null)
                return null;

            var jsonString = JsonConvert.SerializeObject(body);
            return new StringContent(jsonString, encoding: Encoding.UTF8, mediaType: "application/json");
        }
        private void AuthorizeRequest(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
