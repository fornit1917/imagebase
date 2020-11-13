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

        public void Add(Image obj)
        {
            _context.Images.Add(obj);
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
    }
}
