using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ServiceResponse<CatalogDto>> CreateCatalogAsync(CatalogDto catalogDto);
        Task<ServiceResponse<int>> DeleteCatalogAsync(int id, string userId = null);
        Task<ServiceResponse<CatalogDto>> GetCatalogAsync(int id, string userId = null);
        Task<ServiceResponse<CatalogDto>> UpdateCatalogAsync(CatalogDto catalogDto);
        Task<ServiceResponse<UpdateImageCatalogDto>> DeleteImageFromCatalogAsync(UpdateImageCatalogDto imageCatalogDto, string userId = null);
        Task<ServiceResponse<PaginationListDto<ImageDto>>> GetImagesFromCatalogAsync(int id, int offset, int limit, string userId = null);
        Task<ServiceResponse<UpdateImageCatalogDto>> AddImageToCatalogAsync(UpdateImageCatalogDto imageCatalogDto, string userId = null);
        Task<IEnumerable<CatalogDto>> GetCatalogsAsync(string userId = null);
        Task<ServiceResponse<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int parentId, string userId = null);
    }
}
