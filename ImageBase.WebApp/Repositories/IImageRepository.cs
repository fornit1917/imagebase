using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public interface IImageRepository: IRepository<Image, long>
    {
        Task<bool> IsImageTitleAlreadyExists(int id, string title);
        Task<bool> HasCatalogWithUserIdAsync(int? id, string userId);
        Task<IEnumerable<Image>> GetImagesBySearchQueryAsync(FullTextSeacrhDto query);
        Task<int> GetImagesTotalCountBySearchQueryAsync(FullTextSeacrhDto query);
    }
}
