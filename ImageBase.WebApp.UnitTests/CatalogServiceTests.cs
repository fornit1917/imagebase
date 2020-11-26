using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Data.Profiles;
using ImageBase.WebApp.Repositories;
using ImageBase.WebApp.Services;
using ImageBase.WebApp.Services.Implementations;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ImageBase.WebApp.UnitTests
{
    public class CatalogServiceTests
    {
        private Mock<AspPostgreSQLContext> _dbContextMock;
        private Mock<ICatalogRepository> _catalogRepositoryMock;
        private ICatalogService _service;

        public CatalogServiceTests()
        {
            _dbContextMock = new Mock<AspPostgreSQLContext>();
            _catalogRepositoryMock = new Mock<ICatalogRepository>();

            Profile myProfile = new CatalogProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new CatalogService(_catalogRepositoryMock.Object, _dbContextMock.Object, mapper);
        }

        [Fact]
        public async Task GetCatalogAsyncReturnsCatalogDto()
        {
            var catalog = new Catalog()
            {
                Id = 1,
                Name = "catalog 1",
                UserId = "some user"
            };           
            _catalogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(catalog));

            ServiceResponse<CatalogDto> catalogDto = await _service.GetCatalogAsync(1);

            Assert.NotNull(catalogDto);
            Assert.IsAssignableFrom<CatalogDto>(catalogDto);
            Assert.Equal(catalogDto.Data.Id, catalog.Id);
            Assert.Equal(catalogDto.Data.Name, catalog.Name);
            Assert.Equal(catalogDto.Data.UserId, catalog.UserId);
        }
        [Fact]
        public async Task CreateCatalogAsyncCallsMethodOfRepository_And_SavesChanges()
        {            
            await _service.CreateCatalogAsync(new CatalogDto());

            _catalogRepositoryMock.Verify(r => r.Add(It.IsAny<Catalog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task CreateCatalogAsyncReturnsServiceResponse_IfCatalogWithNameExists()
        {
            CatalogDto catalogDto = new CatalogDto();
            _catalogRepositoryMock.Setup(r => r.HasChildWithNameAsync(It.IsAny<Catalog>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<CatalogDto> serviceResponse = await _service.CreateCatalogAsync(catalogDto);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task CreateCatalogAsyncReturnsServiceResponse_IfCatalogWithNameDoesntExsist()
        {
            CatalogDto catalogDto = new CatalogDto();
            _catalogRepositoryMock.Setup(r => r.HasChildWithNameAsync(It.IsAny<Catalog>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<CatalogDto> serviceResponse = await _service.CreateCatalogAsync(catalogDto);

            Assert.True(serviceResponse.Success);
            Assert.Equal(serviceResponse.Data, catalogDto);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteCatalogAsyncCallsMethodOfRepository_And_ReturnsBool(bool flag)
        {
            int id = 1;
            _catalogRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(flag));

            ServiceResponse<int> serviceResponse = await _service.DeleteCatalogAsync(id);

            Assert.Equal(flag, serviceResponse.Success);
            if (flag)
                _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            else
                _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task GetCatalogsAsyncReturnsIEnumerableCatalogDto()
        {
            var catalogs = new Catalog[4];
            _catalogRepositoryMock.Setup(r => r.GetAllAsync()).Returns(Task.FromResult((IEnumerable<Catalog>)catalogs));

            IEnumerable<CatalogDto> catalogDtos = await _service.GetCatalogsAsync();

            Assert.NotNull(catalogDtos);
            Assert.Equal(catalogs.Count(), catalogDtos.Count());
        }

        [Fact]
        public async Task UpdateCatalogAsyncCallsMethodOfRepository_And_SavesChanges()
        {
            var catalog = new CatalogDto()
            {
                Name = "catalog 1",
            };

            ServiceResponse<CatalogDto> serviceResponse = await _service.UpdateCatalogAsync(catalog);

            _catalogRepositoryMock.Verify(r => r.Update(It.IsAny<Catalog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.NotNull(serviceResponse);
        }

        [Fact]
        public async Task DeleteImageFromCatalogAsyncCallsMethodOfRepository_And_SavesChanges()
        {
            var imageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            ImageCatalog ic = new ImageCatalog();           

            await _service.DeleteImageFromCatalogAsync(imageCatalog);

            _catalogRepositoryMock.Verify(r => r.DeleteImageFromCatalog(It.IsAny<ImageCatalog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetImagesByCatalogAsyncReturnsPaginationListDtoImages()
        {
            int id = 1;
            int limit = 4;
            int offset = 0;
            PaginationListDto<Image> paginationList = new PaginationListDto<Image>() { Items = new Image[4]};
            _catalogRepositoryMock.Setup(r => r.GetImagesByCatalogAsync(id, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(paginationList));

            ServiceResponse<PaginationListDto<ImageDto>> serviceResponse = await _service.GetImagesFromCatalogAsync(id, offset, limit);

            _catalogRepositoryMock.Verify(r => r.GetImagesByCatalogAsync(id, offset, limit), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.Equal(paginationList.Items.Count, serviceResponse.Data.Items.Count);
        }

        [Fact]
        public async Task AddImageToCatalogAsyncCallsMethodOfRepository_And_SavesChanges()
        {
            var updateImageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            Expression<Func<ImageCatalog, bool>> ex = i => i.ImageId == updateImageCatalog.ImageId && i.CatalogId == updateImageCatalog.CatalogId;

            await _service.AddImageToCatalogAsync(updateImageCatalog);

            _catalogRepositoryMock.Verify(r => r.AddImageToCatalog(It.Is(ex)), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetSubCatalogsAsyncCallsMethodOfRepository_And_ReturnsCatalogsDto()
        {
            int id = 1;

            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = await _service.GetSubCatalogsAsync(id);

            Assert.NotNull(serviceResponse);
            _catalogRepositoryMock.Verify(r => r.GetSubCatalogsAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetCatalogsByUserAsyncCallsMethodOfRepository_And_ReturnsCatalogsDto()
        {
            var id = "user id";

            IEnumerable<CatalogDto> catalogsDto = await _service.GetCatalogsAsync(id);

            Assert.NotNull(catalogsDto);
            _catalogRepositoryMock.Verify(r => r.GetCatalogsAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetSubCatalogsByUserAsync_IfCatalogDoesntExist()
        {
            _catalogRepositoryMock.Setup(c => c.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = await _service.GetSubCatalogsAsync(It.IsAny<int>(), It.IsAny<string>());

            Assert.NotNull(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetSubCatalogsByUserAsync_IfCatalogExists()
        {
            _catalogRepositoryMock.Setup(c => c.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = await _service.GetSubCatalogsAsync(It.IsAny<int>(), It.IsAny<string>());

            Assert.NotNull(serviceResponse);
            Assert.True(serviceResponse.Success);
        }
    }
}
