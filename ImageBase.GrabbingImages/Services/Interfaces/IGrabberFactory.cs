using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Services.Interfaces
{
    public interface IGrabberFactory<T> where T : class, IImage
    {
        public IConfiguration Configuration { get; }
        public IGrabber<T> UsePexelsGrabber();
    }
}
