using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Services.Interfaces
{
    public interface IGrabberFactory
    {
        public IConfiguration Configuration { get; }
        public IGrabber CreatePexelsGrabber();
    }
}
