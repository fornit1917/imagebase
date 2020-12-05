using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Models;
using System;

namespace ImageBase.GrabbingImages
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
             .AddUserSecrets(typeof(Program).Assembly)
             .Build();
            Grabber grabber = new Grabber(config);
            PhotoPage photoPage = await grabber.SearchPhotosAsync();
        }
    }
}
