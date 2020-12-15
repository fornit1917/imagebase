using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageBase.GrabbingImages.Services.Interfaces
{
    public interface IGrabber
    {
        public IConfiguration Configuration { get; }
        public Task SearchPhotosAsync(string theme, int pagestart, int count, string outputfile);
    }
}
