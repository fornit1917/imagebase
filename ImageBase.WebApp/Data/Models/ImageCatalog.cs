using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models
{
    public class ImageCatalog
    {
        public long ImageId { get; set; }
        public int CatalogId { get; set; }

        public Image Image { get; set; }
        public Catalog Catalog { get; set; }
    }
}
