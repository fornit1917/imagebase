﻿using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Services.Interfaces
{
    public interface IImageService
    {
        Task<ServiceResponse<ImageDto>> CreateImageAsync(AddImageDto addImageDto, string userId = null);
        Task<ServiceResponse<PaginationListDto<ImageDto>>> FullTextSearchByImagesAsync(FullTextSeacrhDto queryDto);
    }
}
