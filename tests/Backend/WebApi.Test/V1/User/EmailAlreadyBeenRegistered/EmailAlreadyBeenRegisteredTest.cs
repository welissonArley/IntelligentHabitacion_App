using FluentAssertions;
using Homuai.Api;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.Builder;
using Xunit;

namespace WebApi.Test.V1.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredTest : BaseControllersTest
    {
        public EmailAlreadyBeenRegisteredTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Validade_Sucess_Response_False()
        {
            var response = await DoGetRequest("user/email-already-been-registered/notregister@notregister.com");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("value").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validade_Sucess_Response_True()
        {
            var response = await DoGetRequest($"user/email-already-been-registered/{EntityBuilder.UserWithoutHome.Email}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("value").GetBoolean().Should().BeTrue();
        }
    }
}
