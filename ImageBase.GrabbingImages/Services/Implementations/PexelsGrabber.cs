﻿using ImageBase.GrabbingImages.Converters;
using ImageBase.GrabbingImages.Dtos;
using ImageBase.GrabbingImages.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageBase.GrabbingImages.Services.Implementations
{
    public class PexelsGrabber : IGrabber
    {
        public IConfiguration Configuration { get; }
        private IConvertToSave Converter { get; set; }
        public PexelsGrabber(IConfiguration configuration)
        {
            Configuration = configuration;
            pexelsClient = new PexelsClient(Configuration.GetConnectionString("MyAPIKey"));
        }
        private PexelsClient pexelsClient;

        public async Task SearchPhotosAsync(string theme, int pagestart, int count)
        {
            PhotoPage photoPage = await pexelsClient.SearchPhotosAsync(theme, "ru-RU", pagestart, count);
            CreateListImages(photoPage);
        }
        private void CreateListImages(PhotoPage photoPage)
        {
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
                Converter.SaveToFile(image);
            }
        }
        public void InitializeConverterStream(IConvertToSave converter, string outputfile)
        {
            Converter = converter;
            Converter.CreateStreamWriter(outputfile);
        }
        public void DisposeStream()
        {
            Converter?.Dispose();

        }
    }
}
