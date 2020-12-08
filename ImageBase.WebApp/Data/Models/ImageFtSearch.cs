using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models
{
    public class ImageFtSearch:BaseEntity<long>
    {
        public long ImageId { get; set; }
        public NpgsqlTsVector ImageVector { get; set; }
        public Image Image { get; set; }
    }
}
