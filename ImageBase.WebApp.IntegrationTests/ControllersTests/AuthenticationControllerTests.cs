using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Dtos.AuthenticationDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImageBase.WebApp.IntegrationTests.ControllersTests
{
    public class AuthenticationControllerTests : IClassFixture<AuthenticationController>
    {
        private ImageBaseWebAppFactory _factory;
        private HttpClient _client;

        [Fact]
        public void GivenARequestToTheController()
        {
            //Act
            _factory = new ImageBaseWebAppFactory();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/api/Authentication")]
        [InlineData("/api/Authentication/register")]
        public async Task AuthenticationAPIEnsure(string url)
        {
            // Arrange
            var response = await _client.GetAsync(url);

            //Act
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task RegisterUserPOSTOK()
        {
            // Arrange
            var content = new RegisterUserDto() { Email = "testemail@mail.ru", Password = "Qwert12345", PasswordConfirm = "Qwert1234" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            //Act
            var result = await _client.PostAsync("/api/Authentication/register", jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }

        [Fact]
        public async Task RegisterUserPOSTBad()
        {
            // Arrange
            var content = new RegisterUserDto();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
           
            //Act
            var result = await _client.PostAsync("/api/Authentication/register", jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }

        [Fact]
        public void TearDown()
        {
            //Act
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
