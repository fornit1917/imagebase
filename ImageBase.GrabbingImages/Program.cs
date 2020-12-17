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
using ImageBase.GrabbingImages.Converters;

namespace ImageBase.GrabbingImages
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                await CreateFactoryAsync(new GrabberFactory());
            }
            else
            {
                switch (args.Length)
                {
                    case 1:
                        if (args[0] == "Pexels")
                        {
                           await CreateFactoryAsync(new GrabberFactory());
                        }
                        break;
                    case 2:
                        if (args[0] == "Pexels")
                        {
                            await CreateFactoryAsync(new GrabberFactory(),args[1]);
                        }
                        break;
                    case 3:
                        if (args[0] == "Pexels")
                        {
                            await CreateFactoryAsync(new GrabberFactory(), args[1],Convert.ToInt32(args[2]));
                        }
                        break;
                    case 4:
                        if (args[0] == "Pexels")
                        {
                            await CreateFactoryAsync(new GrabberFactory(), args[1], Convert.ToInt32(args[2]),args[3]);
                        }
                        break;
                    default:
                        break;
                }
            }
        } 
       
        public static async Task CreateFactoryAsync(IGrabberFactory factory, string theme = "Nature", int countImages = 2, string outputfile = "AllImages.csv")
        {
            var pexelsgrabber =  factory.CreatePexelsGrabber();
            pexelsgrabber.InitializeConverterStream(new CSVConverter(),outputfile);
            await pexelsgrabber.SearchPhotosAsync(theme,1,countImages);
            pexelsgrabber.DisposeStream();
        }
    }
}
