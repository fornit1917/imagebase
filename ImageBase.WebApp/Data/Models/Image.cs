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

        public List<ImageCatalog> ImageCatalogs { get; set; }
    }
}
