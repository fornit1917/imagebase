using AutoMapper;
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
    public class ImageService: IImageService
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

        public async Task CreateImageAsync(AddImageDto imageDto)
        {
            Image image = _mapper.Map<Image>(imageDto);
            _repository.Add(image);
            await _context.SaveChangesAsync();
        }
    }
}
