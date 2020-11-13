using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public interface IRepository<T, TId> 
    {
        Task<bool> DeleteAsync(TId id);
        Task<T> GetAsync(TId id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T obj);
        T Update(T obj);        
    }
}
