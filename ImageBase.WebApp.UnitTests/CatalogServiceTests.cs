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
        public async Task GetCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            var catalog = new Catalog()
            {
                Id = 1,
                Name = "catalog 1",
                UserId = "some user"
            };           
            _catalogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(catalog));
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<CatalogDto> serviceResponse = await _service.GetCatalogAsync(1);

            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<CatalogDto>>(serviceResponse);
            Assert.Equal(serviceResponse.Data.Id, catalog.Id);
            Assert.Equal(serviceResponse.Data.Name, catalog.Name);
            Assert.Equal(serviceResponse.Data.UserId, catalog.UserId);
        }

        [Fact]
        public async Task GetCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<CatalogDto> serviceResponse = await _service.GetCatalogAsync(1);

            _catalogRepositoryMock.Verify(r => r.GetAsync(It.IsAny<int>()), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<CatalogDto>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task CreateCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithNameExists()
        {
            CatalogDto catalogDto = new CatalogDto();
            _catalogRepositoryMock.Setup(r => r.HasChildWithNameAsync(It.IsAny<Catalog>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<CatalogDto> serviceResponse = await _service.CreateCatalogAsync(catalogDto);
            
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<CatalogDto>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }       

        [Fact]
        public async Task CreateCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithNameDoesntExsist()
        {
            CatalogDto catalogDto = new CatalogDto();
            _catalogRepositoryMock.Setup(r => r.HasChildWithNameAsync(It.IsAny<Catalog>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<CatalogDto> serviceResponse = await _service.CreateCatalogAsync(catalogDto);

            _catalogRepositoryMock.Verify(r => r.Add(It.IsAny<Catalog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<CatalogDto>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(catalogDto, serviceResponse.Data);
        }

        [Fact]
        public async Task DeleteCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            int id = 1;
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<int> serviceResponse = await _service.DeleteCatalogAsync(id);

            _catalogRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<int>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(id, serviceResponse.Data);            
        }

        [Fact]
        public async Task DeleteCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            int id = 1;
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<int> serviceResponse = await _service.DeleteCatalogAsync(id);

            _catalogRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<int>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetCatalogsAsyncReturnsIEnumerableCatalogDto()
        {
            var catalogs = new Catalog[4];
            _catalogRepositoryMock.Setup(r => r.GetCatalogsAsync(It.IsAny<string>())).Returns(Task.FromResult((IEnumerable<Catalog>)catalogs));

            IEnumerable<CatalogDto> catalogDtos = await _service.GetCatalogsAsync();

            Assert.NotNull(catalogDtos);
            Assert.Equal(catalogs.Count(), catalogDtos.Count());
        }

        [Fact]
        public async Task UpdateCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithNameExists()
        {            
            _catalogRepositoryMock.Setup(r => r.HasChildWithNameAsync(It.IsAny<Catalog>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<CatalogDto> serviceResponse = await _service.UpdateCatalogAsync(new CatalogDto());

            _catalogRepositoryMock.Verify(r => r.Update(It.IsAny<Catalog>()), Times.Never);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<CatalogDto>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task DeleteImageFromCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            var imageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<UpdateImageCatalogDto> serviceResponse = await _service.DeleteImageFromCatalogAsync(imageCatalog);

            _catalogRepositoryMock.Verify(r => r.DeleteImageFromCatalog(It.IsAny<ImageCatalog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<UpdateImageCatalogDto>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(imageCatalog, serviceResponse.Data);
        }

        [Fact]
        public async Task DeleteImageFromCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            var imageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<UpdateImageCatalogDto> serviceResponse = await _service.DeleteImageFromCatalogAsync(imageCatalog);

            _catalogRepositoryMock.Verify(r => r.DeleteImageFromCatalog(It.IsAny<ImageCatalog>()), Times.Never);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<UpdateImageCatalogDto>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.Null(serviceResponse.Data);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetImagesByCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            int id = 1;
            int limit = 4;
            int offset = 0;
            PaginationListDto<Image> paginationList = new PaginationListDto<Image>() { Items = new Image[4]};
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            _catalogRepositoryMock.Setup(r => r.GetImagesByCatalogAsync(id, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(paginationList));

            ServiceResponse<PaginationListDto<ImagesListItemDto>> serviceResponse = await _service.GetImagesFromCatalogAsync(id, offset, limit);

            _catalogRepositoryMock.Verify(r => r.GetImagesByCatalogAsync(id, offset, limit), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<PaginationListDto<ImagesListItemDto>>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(paginationList.Items.Count, serviceResponse.Data.Items.Count);
        }

        [Fact]
        public async Task GetImagesByCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            int id = 1;
            int limit = 4;
            int offset = 0;
            PaginationListDto<Image> paginationList = new PaginationListDto<Image>() { Items = new Image[4] };
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<PaginationListDto<ImagesListItemDto>> serviceResponse = await _service.GetImagesFromCatalogAsync(id, offset, limit);

            _catalogRepositoryMock.Verify(r => r.GetImagesByCatalogAsync(id, offset, limit), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<PaginationListDto<ImagesListItemDto>>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.Null(serviceResponse.Data);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task AddImageToCatalogAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            var updateImageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            Expression<Func<ImageCatalog, bool>> ex = i => i.ImageId == updateImageCatalog.ImageId && i.CatalogId == updateImageCatalog.CatalogId;

            ServiceResponse<UpdateImageCatalogDto> serviceResponse = await _service.AddImageToCatalogAsync(updateImageCatalog);

            _catalogRepositoryMock.Verify(r => r.AddImageToCatalog(It.Is(ex)), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<UpdateImageCatalogDto>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(updateImageCatalog, serviceResponse.Data);
        }

        [Fact]
        public async Task AddImageToCatalogAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            var updateImageCatalog = new UpdateImageCatalogDto()
            {
                CatalogId = 1,
                ImageId = 1
            };
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));
            Expression<Func<ImageCatalog, bool>> ex = i => i.ImageId == updateImageCatalog.ImageId && i.CatalogId == updateImageCatalog.CatalogId;

            ServiceResponse<UpdateImageCatalogDto> serviceResponse = await _service.AddImageToCatalogAsync(updateImageCatalog);

            _catalogRepositoryMock.Verify(r => r.AddImageToCatalog(It.Is(ex)), Times.Never);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<UpdateImageCatalogDto>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.Null(serviceResponse.Data);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetSubCatalogsAsyncCallsMethOfRepositoryAndReturnsServiceResponse_IfCatalogWithUserExists()
        {
            int id = 1;
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = await _service.GetSubCatalogsAsync(id);

            _catalogRepositoryMock.Verify(r => r.GetSubCatalogsAsync(id), Times.Once);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<IEnumerable<CatalogDto>>>(serviceResponse);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task GetSubCatalogsAsyncReturnsUnsuccesfullServiceResponse_IfCatalogWithUserDoesntExist()
        {
            int id = 1;
            _catalogRepositoryMock.Setup(r => r.HasCatalogWithUserIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = await _service.GetSubCatalogsAsync(id);

            _catalogRepositoryMock.Verify(r => r.GetSubCatalogsAsync(id), Times.Never);
            Assert.NotNull(serviceResponse);
            Assert.IsAssignableFrom<ServiceResponse<IEnumerable<CatalogDto>>>(serviceResponse);
            Assert.False(serviceResponse.Success);
            Assert.Null(serviceResponse.Data);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetCatalogsAsyncCallsMethodOfRepository_And_ReturnsCatalogsDto()
        {
            var id = "user id";

            IEnumerable<CatalogDto> catalogsDto = await _service.GetCatalogsAsync(id);

            Assert.NotNull(catalogsDto);
            _catalogRepositoryMock.Verify(r => r.GetCatalogsAsync(id), Times.Once);
        }
    }
}
