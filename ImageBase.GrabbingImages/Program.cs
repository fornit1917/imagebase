using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Models;
using ImageBase.GrabbingImages.Dtos;
using ImageBase.GrabbingImages.Services.Implementations;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using ServiceStack.Text;
using System;
using ImageBase.GrabbingImages.Services.Interfaces;

namespace ImageBase.GrabbingImages
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                await CreateFabricaAsync(new GrabberFactory());
            }
            else
            {
                switch (args.Length)
                {
                    case 1:
                        if (args[0] == "Pexels")
                        {
                           await CreateFabricaAsync(new GrabberFactory());
                        }
                        break;
                    case 2:
                        if (args[0] == "Pexels")
                        {
                            await CreateFabricaAsync(new GrabberFactory(),args[1]);
                        }
                        break;
                    case 3:
                        if (args[0] == "Pexels")
                        {
                            await CreateFabricaAsync(new GrabberFactory(), args[1],Convert.ToInt32(args[2]));
                        }
                        break;
                    case 4:
                        if (args[0] == "Pexels")
                        {
                            await CreateFabricaAsync(new GrabberFactory(), args[1], Convert.ToInt32(args[2]),args[3]);
                        }
                        break;
                    default:
                        break;
                }
            }
        } 
        static void ConvertToCSVAndSaveInFile(List<ImageDto> imageDtos,string destination)
        {
            using (var sw = new StreamWriter(destination))
            {
                sw.WriteCsv(imageDtos);
            }
        }

        public static async Task CreateFabricaAsync(IGrabberFactory<ImageDto> factory, string theme = "Nature", int countImages = 5, string outputfile = "AllImages2.csv")
        {
            var pexelsgrabber =  factory.UsePexelsGrabber();
            List<ImageDto> _listImageDtos = await pexelsgrabber.SearchPhotosAsync(theme,1,countImages);
            ConvertToCSVAndSaveInFile(_listImageDtos, outputfile);
        }
    }
}
