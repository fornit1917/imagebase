using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class FullTextSeacrhDto
    {
        public string SearchQuery { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public bool IncludeTitle { get; set; }
        public bool IncludeDescription { get; set; }
        public bool IncludeKeyWords { get; set; }

    }
}
