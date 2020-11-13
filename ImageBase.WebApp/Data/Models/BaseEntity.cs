using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models
{
    public class BaseEntity<TId>
        where TId: IComparable
    {
        public TId Id { get; set; }
    }
}
