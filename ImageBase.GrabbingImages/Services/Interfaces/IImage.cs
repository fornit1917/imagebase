using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Services.Interfaces
{
    public interface IImage
    {
        public int id { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string url { get; set; }
    }
}
