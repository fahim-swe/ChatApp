using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<CreateAccountDto, AppUser>();
            CreateMap<UserDto, UserDto>();
            CreateMap<AppUser, AppUserDto>();
    
        }
    }
}