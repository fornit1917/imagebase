using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageBase.GrabbingImages.Services.Interfaces
{
    public interface IGrabber<T> where T:class, IImage
    {
        public IConfiguration Configuration { get; }
        public Task<List<T>> SearchPhotosAsync(string theme, int pagestart, int count);
    }
}
