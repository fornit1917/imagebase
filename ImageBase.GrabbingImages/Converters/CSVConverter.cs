using ImageBase.GrabbingImages.Dtos;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageBase.GrabbingImages.Converters
{
    public class CSVConverter : IConvertToSave
    {
        public StreamWriter CreateStreamWriter(string OutputFile)
        {
            StreamWr = new StreamWriter(OutputFile, true);
            return StreamWr;
        }
        public static string OutputFile { get; set; }
        private static StreamWriter StreamWr { get; set; }

        public void SaveToFile(ImageDto imageDto)
        {
            StreamWr.WriteCsv(imageDto.InList());
        }

        public void Dispose()
        {
            StreamWr?.Dispose();
        }

    }
}
