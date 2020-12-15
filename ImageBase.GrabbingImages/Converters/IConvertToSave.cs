using ImageBase.GrabbingImages.Dtos;

namespace ImageBase.GrabbingImages.Converters
{
    public interface IConvertToSave
    {
        public void SaveToFile(ImageDto imageDto);
    }
}