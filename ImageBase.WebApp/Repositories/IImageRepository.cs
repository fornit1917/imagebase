using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public interface IImageRepository: IRepository<Image, long>
    {
        Task<bool> IsImageTitleAlreadyExists(int id, string title);
        Task<bool> HasCatalogWithUserIdAsync(int? id, string userId);
        Task<PaginationListDto<Image>> GetImagesBySearchQueryAsync(FullTextSeacrhDto query);
    }
}
