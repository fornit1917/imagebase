using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            Assert.Equal("application/json; charset=utf-8",
        response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task RegisterUserPOSTOK()
        {
            var content = new RegisterUserDto() { Email = "testemail@mail.ru", Password = "123ASD!asd", PasswordConfirm = "123ASD!asd" };

            var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/authentication/register", jsonContent);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task RegisterUserPOSTBad()
        {
            var content = new RegisterUserDto();

            var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");                       
            var result = await _client.PostAsync("/api/authentication/register", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
