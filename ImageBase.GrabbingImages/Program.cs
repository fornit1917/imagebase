using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Models;
using ImageBase.GrabbingImages.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using ServiceStack.Text;
using System;

namespace ImageBase.GrabbingImages
{
    public class Program
    {
        private static List<ImageDto> _listImageDtos;

        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
               await GrabbingFromPexels();
            }
            else
            {
                switch (args.Length)
                {
                    case 1:
                        if (args[0] == "Pexels")
                        {
                            await GrabbingFromPexels();
                        }
                        break;
                    case 2:
                        if (args[0] == "Pexels")
                        {
                            await GrabbingFromPexels(args[1]);
                        }
                        break;
                    case 3:
                        if (args[0] == "Pexels")
                        {
                            await GrabbingFromPexels(args[1],Convert.ToInt32(args[2]));
                        }
                        break;
                    case 4:
                        if (args[0] == "Pexels")
                        {
                            await GrabbingFromPexels(args[1], Convert.ToInt32(args[2]),args[3]);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        static async Task GrabbingFromPexels(string theme="Nature",int countImages=5,string outputfile= "AllImages.csv")
        {
            Grabber grabber = InicializeGrabber();
            PhotoPage photoPage = await grabber.SearchPhotosAsync(theme, 1, countImages);
            _listImageDtos = CreateListImages(photoPage);
            ConvertToCSVAndSaveInFile(_listImageDtos, outputfile);
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
                sw.WriteCsv(imageDtos);
            }
        }
    }
}
