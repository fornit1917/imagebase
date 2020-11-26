using AutoMapper;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Repositories;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Services.Implementations
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _repository;
        private readonly AspPostgreSQLContext _context;
        private readonly IMapper _mapper;

        public CatalogService(ICatalogRepository repository, AspPostgreSQLContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CatalogDto>> CreateCatalogAsync(CatalogDto catalogDto)
        {
            ServiceResponse<CatalogDto> serviceResponse = new ServiceResponse<CatalogDto>();

            if (catalogDto.ParentCatalogId == null ||
                await _repository.HasCatalogWithUserIdAsync(catalogDto.ParentCatalogId, catalogDto.UserId))
            {
                Catalog catalog = _mapper.Map<Catalog>(catalogDto);
                if (await _repository.HasChildWithNameAsync(catalog))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"The catalog with a name {catalogDto.Name} already exists";
                }
                else
                {
                    _repository.Add(catalog);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = catalogDto;
                }
                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = "Access is forbidden or catalog is not found";

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> DeleteCatalogAsync(int id, string userId = null)
        {
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();

            if (await _repository.HasCatalogWithUserIdAsync(id, userId))
            {
                await _repository.DeleteAsync(id);
                await _context.SaveChangesAsync();
                serviceResponse.Data = id;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }            
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<CatalogDto>> GetCatalogAsync(int id, string userId = null)
        {
            ServiceResponse<CatalogDto> serviceResponse = new ServiceResponse<CatalogDto>();

            if (await _repository.HasCatalogWithUserIdAsync(id, userId))
                serviceResponse.Data = _mapper.Map<CatalogDto>(await _repository.GetAsync(id));
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CatalogDto>> UpdateCatalogAsync(CatalogDto catalogDto)
        {
            ServiceResponse<CatalogDto> serviceResponse = new ServiceResponse<CatalogDto>();            

            if (await _repository.HasCatalogWithUserIdAsync(catalogDto.Id, catalogDto.UserId))
            {
                Catalog catalog = _mapper.Map<Catalog>(catalogDto);
                if (await _repository.HasChildWithNameAsync(catalog))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Catalog with a name {catalogDto.Name} already exists";
                }
                else
                {
                    _repository.Update(catalog);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = catalogDto;
                }
                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = "Access is forbidden or catalog is not found";

            return serviceResponse;
        }

        public async Task<ServiceResponse<UpdateImageCatalogDto>> DeleteImageFromCatalogAsync(UpdateImageCatalogDto imageCatalogDto, string userId = null)
        {
            ServiceResponse<UpdateImageCatalogDto> serviceResponse = new ServiceResponse<UpdateImageCatalogDto>();

            if (await _repository.HasCatalogWithUserIdAsync(imageCatalogDto.CatalogId, userId))
            {
                ImageCatalog imageCatalog = await _repository.GetImageCatalogByIdFKAsync(imageCatalogDto.ImageId, imageCatalogDto.CatalogId);
                _repository.DeleteImageFromCatalog(imageCatalog);
                await _context.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<PaginationListDto<ImageDto>>> GetImagesFromCatalogAsync(int id, int offset, int limit, string userId = null)
        {
            ServiceResponse<PaginationListDto<ImageDto>> serviceResponse = new ServiceResponse<PaginationListDto<ImageDto>>();

            if (await _repository.HasCatalogWithUserIdAsync(id, userId))
            {
                PaginationListDto<Image> paginationListDto = await _repository.GetImagesByCatalogAsync(id, offset, limit);
                serviceResponse.Data = _mapper.Map<PaginationListDto<Image>, PaginationListDto<ImageDto>>(paginationListDto);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UpdateImageCatalogDto>> AddImageToCatalogAsync(UpdateImageCatalogDto imageCatalogDto, string userId = null)
        {
            ServiceResponse<UpdateImageCatalogDto> serviceResponse = new ServiceResponse<UpdateImageCatalogDto>();

            if (await _repository.HasCatalogWithUserIdAsync(imageCatalogDto.CatalogId, userId))
            {
                ImageCatalog imageCatalog = new ImageCatalog()
                {
                    ImageId = imageCatalogDto.ImageId,
                    CatalogId = imageCatalogDto.CatalogId
                };
                _repository.AddImageToCatalog(imageCatalog);
                await _context.SaveChangesAsync();

                serviceResponse.Data = imageCatalogDto;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }
            return serviceResponse;
        }

        public async Task<IEnumerable<CatalogDto>> GetCatalogsAsync(string userId = null)
        {
            IEnumerable<CatalogDto> catalogsDto = _mapper.Map<IEnumerable<Catalog>, IEnumerable<CatalogDto>>(await _repository.GetCatalogsAsync(userId));
            return catalogsDto;
        }

        public async Task<ServiceResponse<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int parentId, string userId = null)
        {
            ServiceResponse<IEnumerable<CatalogDto>> serviceResponse = new ServiceResponse<IEnumerable<CatalogDto>>();

            if (await _repository.HasCatalogWithUserIdAsync(parentId, userId))
                serviceResponse.Data = _mapper.Map<IEnumerable<Catalog>, IEnumerable<CatalogDto>>(await _repository.GetSubCatalogsAsync(parentId));
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Access is forbidden or catalog is not found";
            }

            return serviceResponse;
        }
    }
}
