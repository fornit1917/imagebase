using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Services.Implementations;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImageBase.WebApp.UnitTests.ControllersTest
{
    public class ImagesControllerTest
    {
        private ImagesController _imagesController;

        public ImagesControllerTest()
        {
            var _loggerMock = new Mock<ILogger<ImagesController>>();
            var _imageservicerMock = new Mock<IImageService>();
            _imagesController = new ImagesController(_loggerMock.Object,_imageservicerMock.Object);
        }

        [Fact]
        public async Task CreateImageActionReturnsResultWithImageDto_OnSuccededAdd()
        {
            var imageDto = new AddImageDto() { CatalogsIds = null, Description = "Image_Description_1", KeyWords = "Nature, Sun", Title = "Sun" };

            var act = await _imagesController.CreateImage(imageDto);

            Assert.NotNull(act);
            Assert.IsType<OkResult>(act.Result);
        }

        //[Fact]
        //public async Task CreateImageActionReturnsResultWithImageDto_OnFailAdd()
        //{
        //    var imageDto = new AddImageDto() {};

        //    var act = await _imagesController.CreateImage(imageDto);

        //    Assert.NotNull(act);
        //    Assert.IsType<BadRequestResult>(act.Result);
        //}
    }
}
