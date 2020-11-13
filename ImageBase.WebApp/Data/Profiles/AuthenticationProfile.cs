using AutoMapper;
using ImageBase.WebApp.Data.Dtos.AuthenticationDto;
using ImageBase.WebApp.Data.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<LoginUserDto, User>();
            CreateMap<RegisterUserDto, User>();
        }

    }
}
