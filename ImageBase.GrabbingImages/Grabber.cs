using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageBase.GrabbingImages
{
    public class Grabber
    {
        public IConfiguration Configuration { get; }
        public Grabber(IConfiguration configuration)
        {
            Configuration = configuration;
            pexelsClient = new PexelsClient(Configuration.GetConnectionString("MyAPIKey"));
        }
        private PexelsClient pexelsClient;

        public async Task<PhotoPage> SearchPhotosAsync(string tema = "Nature", int pagestart=1,int count = 1)
        {
            PhotoPage photoPage = await pexelsClient.SearchPhotosAsync(tema, "ru-RU", pagestart, count);
            return photoPage;
        }
    }
}
