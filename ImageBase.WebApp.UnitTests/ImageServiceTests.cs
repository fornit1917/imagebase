using AutoMapper;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Data.Profiles;
using ImageBase.WebApp.Repositories;
using ImageBase.WebApp.Services.Implementations;
using ImageBase.WebApp.Services.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ImageBase.WebApp.UnitTests
{
    public class ImageServiceTests
    {
        private Mock<AspPostgreSQLContext> _dbContextMock;
        private Mock<IImageRepository> _imageRepositoryMock;
        private IImageService _service;

        public ImageServiceTests()
        {
            _dbContextMock = new Mock<AspPostgreSQLContext>();
            _imageRepositoryMock = new Mock<IImageRepository>();

            Profile myProfile = new CatalogProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new ImageService(_imageRepositoryMock.Object, _dbContextMock.Object, mapper);
        }

        [Fact]
        public async Task CreatImageAsyncCallsMethodOfRepository_And_SavesChanges()
        {
            await _service.CreateImageAsync(new AddImageDto());

            _imageRepositoryMock.Verify(r => r.Add(It.IsAny<Image>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

    }
}
