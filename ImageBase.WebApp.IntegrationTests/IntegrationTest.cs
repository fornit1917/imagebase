using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ImageBase.WebApp.IntegrationTests
{
    public class IntegrationTest : IClassFixture<ImageBaseWebAppFactory>
    {

        protected readonly ImageBaseWebAppFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public IntegrationTest(ImageBaseWebAppFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                  .AddJsonFile("integrationsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"integrationsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                  .AddJsonFile($"integrationsettings.{Environment.MachineName}.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();
        }
    }
}
