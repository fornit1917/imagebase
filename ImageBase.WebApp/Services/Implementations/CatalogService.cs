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

        public async Task CreateCatalogAsync(CatalogDto catalogDto)
        {
            Catalog catalog = _mapper.Map<Catalog>(catalogDto);
            _repository.Add(catalog);
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> DeleteCatalogAsync(int id)
        {
            bool flag = await _repository.DeleteAsync(id);
            if (flag)
                await _context.SaveChangesAsync();
            return flag;           
        }

        public async Task<CatalogDto> GetCatalogAsync(int id)
        {
            CatalogDto catalogDto = _mapper.Map<CatalogDto>(await _repository.GetAsync(id));
            return catalogDto;
        }

        public async Task<IEnumerable<CatalogDto>> GetCatalogsAsync()
        {
            IEnumerable<CatalogDto> catalogsDto = _mapper.Map<IEnumerable<Catalog>, IEnumerable<CatalogDto>>(await _repository.GetAllAsync());
            return catalogsDto;
        }

        public async Task<IEnumerable<CatalogDto>> GetSubCatalogsAsync(int id)
        {
            IEnumerable<CatalogDto> catalogsDto = _mapper.Map<IEnumerable<Catalog>, IEnumerable<CatalogDto>>(await _repository.GetSubCatalogsAsync(id));
            return catalogsDto;
        }

        public async Task<CatalogDto> UpdateCatalogAsync(int id, CatalogDto catalogDto)
        {
            Catalog catalog = _mapper.Map<Catalog>(catalogDto);
            catalog.Id = id;
            _repository.Update(catalog);
            await _context.SaveChangesAsync();
            return catalogDto;
        }

        public async Task DeleteImageFromCatalogAsync(UpdateImageCatalogDto imageCatalogDto)
        {
            ImageCatalog imageCatalog = await _repository.GetImageCatalogByIdFKAsync(imageCatalogDto.ImageId, imageCatalogDto.CatalogId);
            _repository.DeleteImageFromCatalog(imageCatalog);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginationListDto<ImageDto>> GetImagesByCatalogAsync(int id, int offset, int limit)
        {
            PaginationListDto<Image> paginationListDto = await _repository.GetImagesByCatalogAsync(id, offset, limit);

            return _mapper.Map<PaginationListDto<Image>, PaginationListDto<ImageDto>>(paginationListDto);
        }

        public async Task AddImageToCatalogAsync(UpdateImageCatalogDto imageCatalogDto)
        {
            ImageCatalog imageCatalog = new ImageCatalog()
            {
                ImageId = imageCatalogDto.ImageId,
                CatalogId = imageCatalogDto.CatalogId
            };
            _repository.AddImageToCatalog(imageCatalog);
            await _context.SaveChangesAsync();
        }
    }
}
