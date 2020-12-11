using ImageBase.GrabbingImages.Dtos;
using ImageBase.GrabbingImages.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Services.Implementations
{
    public class GrabberFactory : IGrabberFactory<ImageDto>
    {
        public GrabberFactory()
        {
            IConfiguration config = new ConfigurationBuilder()
             .AddUserSecrets(typeof(Program).Assembly)
             .Build();
            Configuration = config;
        }
        public IConfiguration Configuration { get; }
        public IGrabber<ImageDto> UsePexelsGrabber()
        {
            return new PexelsGrabber(Configuration);
        }
    }
}
