using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class UpdateImageCatalogDto
    {
        public long ImageId { get; set; }
        public int CatalogId { get; set; }
    }
}
