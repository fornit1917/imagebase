using ImageBase.WebApp.Data.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models
{
    public class Catalog: BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentCatalogId { get; set; }
        public string UserId { get; set; }

        public Catalog ParentCatalog { get; set; }        
        public User User { get; set; }
        public List<ImageCatalog> ImageCatalogs { get; set; }
    }
}
