using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Dtos.AuthenticationDto;
using ImageBase.WebApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ImageBase.WebApp.IntegrationTests.ControllersTests
{
    public class AuthenticationControllerTests : IntegrationTest
    {
        public AuthenticationControllerTests(ImageBaseWebAppFactory fixture)
          : base(fixture) { }

        [Theory]
        [InlineData("/")]
        [InlineData("/api/authentication/")]
        [InlineData("/api/authentication/register")]
        public async Task GetAPIReturnSuccessAndCorrectContentType(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseReturn = JsonConvert.DeserializeObject<Response>(responseString);

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task RegisterUserPOSTOK()
        {
            var content = new RegisterUserDto() { Email = "testemail229@mail.ru", Password = "123ASD!asd", PasswordConfirm = "123ASD!asd" };
            
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/authentication/register", jsonContent);

            var responseString = await result.Content.ReadAsStringAsync();
            var responseReturn = JsonConvert.DeserializeObject<Response>(responseString);

            Assert.True(responseReturn.Status == "Success", "Expected added user to have a valid id");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task RegisterUserPOSTBad()
        {
            var content = new RegisterUserDto();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/authentication/register", jsonContent);

            var responseString = await result.Content.ReadAsStringAsync();
            var responseReturn = JsonConvert.DeserializeObject<Response>(responseString);

            Assert.True(responseReturn.Status == "400", "Expected added user to have an error id");
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
