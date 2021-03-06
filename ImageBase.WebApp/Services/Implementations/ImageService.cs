﻿using AutoMapper;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Repositories;
using ImageBase.WebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        private readonly AspPostgreSQLContext _context;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository repository, AspPostgreSQLContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ImageDto>> CreateImageAsync(AddImageDto addImageDto, string userId = null)
        {
            ServiceResponse<ImageDto> serviceResponse = new ServiceResponse<ImageDto>();

            foreach (var id in addImageDto.CatalogsIds)
            {
                if (!await _repository.HasCatalogWithUserIdAsync(id, userId))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Access is forbidden or catalog - {id} is not found";
                    return serviceResponse;
                }
            }
            Image image = _mapper.Map<Image>(addImageDto);
            image.Id = _repository.Add(image);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<ImageDto>(image);

            return serviceResponse;
        }

        public async Task<ServiceResponse<PaginationListDto<ImageDto>>> FullTextSearchByImagesAsync(FullTextSeacrhDto queryDto)
        {
            var serviceResponse = new ServiceResponse<PaginationListDto<ImageDto>>();

            
            var images= await _repository.GetImagesBySearchQueryAsync(queryDto);
            var imagesDtos = new List<ImageDto>(images.Count());
            foreach (Image img in images)
            {
                imagesDtos.Add(_mapper.Map<ImageDto>(img));
            }
            serviceResponse.Data = new PaginationListDto<ImageDto> { Items=imagesDtos,Limit=queryDto.Limit,Offset=queryDto.Offset,TotalItemsCount=await _repository.GetImagesTotalCountBySearchQueryAsync(queryDto)};
            serviceResponse.Success = true;
            return serviceResponse;
        }
    }
}
