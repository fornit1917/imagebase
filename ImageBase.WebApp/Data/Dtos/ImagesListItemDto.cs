using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class ImagesListItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SmallPreviewUrl { get; set; }
        public string LargePreviewUrl { get; set; }
        public string OriginalUrl { get; set; }
    }
}
