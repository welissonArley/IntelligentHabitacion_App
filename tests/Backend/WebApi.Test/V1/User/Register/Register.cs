using FluentAssertions;
using Homuai.Api;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace WebApi.Test.V1.User.Register
{
    public class Register : BaseControllersTest
    {
        public Register(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUser.Instance().Build();

            var response = await DoPostRequest("user", user);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
            responseData.RootElement.GetProperty("profileColorLightMode").GetString().Should().NotBeNullOrWhiteSpace();
            responseData.RootElement.GetProperty("profileColorDarkMode").GetString().Should().NotBeNullOrWhiteSpace();
        }
    }
}
