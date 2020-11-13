using ImageBase.WebApp.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Services.Interfaces
{
    public interface IImageService
    {
        Task CreateImageAsync(AddImageDto imageDto);
    }
}
