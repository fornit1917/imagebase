using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public interface ICatalogRepository: IRepository<Catalog, int>
    {
        Task<IEnumerable<Catalog>> GetSubCatalogsAsync(int id);
        Task<PaginationListDto<Image>> GetImagesByCatalogAsync(long id, int offset, int limit);
        void AddImageToCatalog(ImageCatalog imageCatalog);
        void DeleteImageFromCatalog(ImageCatalog imageCatalog);
        Task<ImageCatalog> GetImageCatalogByIdFKAsync(long idImg, int idCat);
    }
}
