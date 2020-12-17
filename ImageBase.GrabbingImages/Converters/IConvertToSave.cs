using ImageBase.GrabbingImages.Dtos;
using System;
using System.IO;

namespace ImageBase.GrabbingImages.Converters
{
    public interface IConvertToSave: IDisposable
    {
        public void SaveToFile(ImageDto imageDto);
        public StreamWriter CreateStreamWriter(string outputfile);
    }
}