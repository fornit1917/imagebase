using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Models;
using ImageBase.GrabbingImages.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace ImageBase.GrabbingImages
{
    public class Program
    {
        private static List<ImageDto> _listImageDtos;
        private static string KeyWord;
        static async Task Main(string[] args)
        {
            Grabber grabber = InicializeGrabber();
            KeyWord = "Nature";
            PhotoPage photoPage = await grabber.SearchPhotosAsync(KeyWord, 1,5);

            _listImageDtos = CreateListImages(photoPage);
            ConvertToCSVAndSaveInFile(_listImageDtos,"AllImages.csv");
        }
        static Grabber InicializeGrabber()
        {
            IConfiguration config = new ConfigurationBuilder()
             .AddUserSecrets(typeof(Program).Assembly)
             .Build();
            Grabber grabber = new Grabber(config);
            return grabber;
        }
        static List<ImageDto> CreateListImages(PhotoPage photoPage)
        {
            List<ImageDto> listImageDtos = new List<ImageDto>();

            for (int i = 0; i < photoPage.photos.Count; i++)
            {
                ImageDto image = new ImageDto()
                {
                    id = photoPage.photos[i].id,
                    width = photoPage.photos[i].width,
                    height = photoPage.photos[i].height,
                    url = photoPage.photos[i].url,
                    photographer = photoPage.photos[i].photographer,
                    original = photoPage.photos[i].source.original,
                    large = photoPage.photos[i].source.large,
                    medium = photoPage.photos[i].source.medium,
                    small = photoPage.photos[i].source.small,
                    landscape = photoPage.photos[i].source.landscape
                };
                listImageDtos.Add(image);
            }
            return listImageDtos;           
        }

        static void ConvertToCSVAndSaveInFile(List<ImageDto> imageDtos,string destination)
        {
            using (var sw = new StreamWriter(destination))
            {
                foreach (var item in imageDtos)
                {
                    sw.WriteLine(item.ToCsv());
                }
            }
        }
    }
}
