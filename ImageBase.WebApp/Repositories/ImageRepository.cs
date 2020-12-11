using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PaginationListDto<Image>> GetImagesBySearchQueryAsync(FullTextSeacrhDto query)
        {
            var weights=query.ConvertToPostgreFTSWeights();
            var images = await _context.Images
                .FromSqlInterpolated($@"SELECT * FROM images_fts({query.SearchQuery},{weights},{query.Limit},{query.Offset});").ToListAsync();
            return new PaginationListDto<Image>
            {
                Items = images,
                TotalItemsCount = images.Count(),
                Limit=query.Limit,
                Offset=query.Offset              
            };
        }
    }
}
