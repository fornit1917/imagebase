using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public class CatalogRepository: ICatalogRepository
    {
        private AspPostgreSQLContext _context;

        public CatalogRepository(AspPostgreSQLContext context)
        {
            _context = context;
        }
        public void Add(Catalog obj)
        {
            _context.Catalogs.Add(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _context.Catalogs.SingleAsync(p => p.Id.CompareTo(id) == 0);
            if (obj is null)
            {
                return false;
            }
            _context.Catalogs.Remove(obj);

            return true;
        }

        public async Task<Catalog> GetAsync(int id)
        {
            return await _context.Catalogs.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            return await _context.Catalogs.ToArrayAsync();
        }

        public async Task<IEnumerable<Catalog>> GetSubCatalogsAsync(int id)
        {
            return await _context.Catalogs.Where(c => c.ParentCatalogId == id).ToArrayAsync();
        }

        public Catalog Update(Catalog obj)
        {
            _context.Catalogs.Update(obj);
            return obj;
        }

        public void DeleteImageFromCatalog(ImageCatalog imageCatalog)
        {            
            _context.ImageCatalogs.Remove(imageCatalog);
        }

        public async Task<PaginationListDto<Image>> GetImagesByCatalogAsync(long id, int offset, int limit)
        {
            IQueryable<Image> query = _context.ImageCatalogs.Where(ic => ic.CatalogId == id)
                .Include(ic => ic.Image)
                .Select(i => i.Image);

            var totalCount = await query.CountAsync();
            Image[] list = await query.OrderBy(i => i.Id)
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();

            var paginationList = new PaginationListDto<Image>()
            {
                Items = list,
                Limit = limit,
                Offset = offset,
                TotalItemsCount = totalCount
            };

            return paginationList;
        }

        public void AddImageToCatalog(ImageCatalog imageCatalog)
        {
            _context.ImageCatalogs.Add(imageCatalog);
        }

        public async Task<ImageCatalog> GetImageCatalogByIdFKAsync(long idImg, int idCat)
        {
            return await _context.ImageCatalogs.Where(ic => ic.ImageId == idImg && ic.CatalogId == idCat).FirstOrDefaultAsync();
        }
    }
}
