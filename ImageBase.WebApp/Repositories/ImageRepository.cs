using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private AspPostgreSQLContext _context;
        public ImageRepository(AspPostgreSQLContext context)
        {
            _context = context;
        }

        public long Add(Image obj)
        {
            _context.Images.Add(obj);
            return obj.Id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var obj = await _context.Images.SingleAsync(p => p.Id.CompareTo(id) == 0);
            if (obj is null)
            {
                return false;
            }
            _context.Images.Remove(obj);

            return true;
        }

        public async Task<Image> GetAsync(long id)
        {
            return await _context.Images.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Images.ToArrayAsync();
        }

        public Image Update(Image obj)
        {
            _context.Images.Update(obj);
            return obj;
        }

        public async Task<bool> IsImageTitleAlreadyExists(int id, string title)
        {
            bool isExist = await _context.ImageCatalogs.Where(ic => ic.CatalogId == id)
                .Include(ic => ic.Image)
                .Select(i => i.Image)
                .AnyAsync(i => i.Title == title);
            return isExist;
        }

        public async Task<bool> HasCatalogWithUserIdAsync(int? id, string userId)
        {
            return await _context.Catalogs.Where(c => c.Id == id && c.UserId == userId).AnyAsync();
        }

        public async Task<IEnumerable<Image>> GetImagesBySearchQueryAsync(FullTextSeacrhDto query)
        {
            var weights=ConvertToPostgreFTSWeights(query);
            var images = await _context.Images
                .FromSqlInterpolated($@"SELECT * FROM search_images_ft({query.SearchQuery},{weights},{query.Limit},{query.Offset});").ToListAsync();
            return images;
        }
        public async Task<int> GetImagesTotalCountBySearchQueryAsync(FullTextSeacrhDto query)
        {
            var result = await _context.Images.FromSqlInterpolated($@"SELECT * FROM get_search_images_ft_total({query.SearchQuery})").CountAsync();
            return result;
        }
        public string ConvertToPostgreFTSWeights(FullTextSeacrhDto query)
        {
            string result = "";
            if (query.IncludeTitle) result += "A";
            if (query.IncludeKeyWords) result += "B";
            if (query.IncludeDescription) result += "C";
            return result;
        }
    }
}
