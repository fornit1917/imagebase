using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public interface IImageRepository: IRepository<Image, long>
    {
    }
}
