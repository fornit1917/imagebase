using AutoMapper;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<CatalogDto, Catalog>().ReverseMap();
            CreateMap<AddImageDto, Image>()
                .ForMember(map => map.ImageCatalogs, map => map.MapFrom(i => i.CatalogsIds));
            CreateMap<long, ImageCatalog>()
                .ForMember(map => map.CatalogId, map => map.MapFrom(c => c));
            CreateMap<PaginationListDto<Image>, PaginationListDto<ImageDto>>();
        }
    }
}
