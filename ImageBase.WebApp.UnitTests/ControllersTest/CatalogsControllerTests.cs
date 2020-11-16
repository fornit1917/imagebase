using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Data.Dtos;
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
    public class CatalogsControllerTests
    {
        private CatalogsController _catalogsController;

        public CatalogsControllerTests()
        {
            var _loggerMock = new Mock<ILogger<CatalogsController>>();
            var _imageservicerMock = new Mock<ICatalogService>();
            _catalogsController = new CatalogsController(_loggerMock.Object, _imageservicerMock.Object);
        }

        [Fact]
        public async Task GetCatalogsActionReturnsResultWithCatalogDto_OnSuccededReturn()
        {
            var act = await _catalogsController.GetCatalogs();

            Assert.NotNull(act);
            Assert.IsType<OkObjectResult>(act.Result);
        }

        [Fact]
        public async Task GetSubCatalogsActionReturnsResultWithCatalogDto_OnSuccededReturn()
        {
            var act = await _catalogsController.GetSubCatalogsAsync(0);

            Assert.NotNull(act);
            Assert.IsType<OkObjectResult>(act.Result);
        }

        [Fact]
        public async Task GetCatalogActionReturnsResultWithCatalogDto_OnNotFoundReturn()
        {
            var act = await _catalogsController.GetCatalog(0);

            Assert.NotNull(act);
            Assert.IsType<NotFoundResult>(act.Result);
        }

        [Fact]
        public async Task UpdateCatalogActionReturnsResultWithCatalogDto_OnSuccededReturn()
        {
            var catalogDto = new CatalogDto() {Id = 0, Name = "Nature", ParentCatalogId = null, UserId = "0"};

            var act = await _catalogsController.UpdateCatalog(0,catalogDto);

            Assert.NotNull(act);
            Assert.IsType<OkObjectResult>(act.Result);
        }


        [Fact]
        public async Task CreateCatalogActionReturnsResultWithCatalogDto_OnSuccededReturn()
        {
            var catalogDto = new CatalogDto() { Id = 0, Name = "Nature", ParentCatalogId = null, UserId = "0" };

            var act = await _catalogsController.CreateCatalog(catalogDto);

            Assert.NotNull(act);
            Assert.IsType<OkResult>(act.Result);
        }
    }
}
