using ImageBase.WebApp.IntegrationTests.DataTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;

namespace ImageBase.WebApp.IntegrationTests
{
    public class ImageBaseWebAppFactory : WebApplicationFactory<Startup>
    {

        public IConfiguration Configuration { get; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationConfig = new ConfigurationBuilder()
                  .AddJsonFile("integrationsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"integrationsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                  .AddJsonFile($"integrationsettings.{Environment.MachineName}.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();

                config.AddConfiguration(integrationConfig);
            });
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContextPool<AspPostrgreSQLContextTest>(options =>
                      options.UseNpgsql(Configuration.GetConnectionString("AspPostrgreSQLContextTest")));
                services.AddControllers().AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            });
        }
    }
}
