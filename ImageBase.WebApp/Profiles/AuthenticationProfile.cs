using AutoMapper;
using ImageBase.WebApp.Dtos.AuthenticationDto;
using ImageBase.WebApp.Models.Authentication;
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
