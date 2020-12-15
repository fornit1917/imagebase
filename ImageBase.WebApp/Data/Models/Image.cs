using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models
{
    public class Image: BaseEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public ImageFtSearch ImageFtSearch { get; set; }
        public int ServiceId { get; set; }
        public string ExternalId { get; set; }
        public string SmallPreviewUrl { get; set; }
        public string LargePreviewUrl { get; set; }
        public string OriginalUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FileSize { get; set; }
        public List<ImageCatalog> ImageCatalogs { get; set; }
    }
}
