using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class PaginationListDto<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int TotalItemsCount { get; set; }        
    }
}

