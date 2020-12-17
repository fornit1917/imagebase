using ImageBase.GrabbingImages.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Dtos
{
    public class ImageDto
    {
        public int id { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string url { get; set; }

        public string photographer { get; set; }

        public string original { get; set; }

        public string large { get; set; }

        public string medium { get; set; }

        public string small { get; set; }

        public string landscape { get; set; }
    }
}
