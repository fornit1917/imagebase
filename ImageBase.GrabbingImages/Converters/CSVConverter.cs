using ImageBase.GrabbingImages.Dtos;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageBase.GrabbingImages.Converters
{
    public class CSVConverter : IConvertToSave
    {
        public string OutputFile { get; set; }
        public void SaveToFile(ImageDto imageDto)
        {
            using (var sw = new StreamWriter(OutputFile,true))
            {
                sw.WriteCsv(imageDto.InList());
            }
        }
    }
}
