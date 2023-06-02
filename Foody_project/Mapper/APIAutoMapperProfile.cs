using AutoMapper;
using Foody_project.DTOs;
using Foody_project.Models;
using FoodyDl.FoodyService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foody_project.Mapper
{
    public class APIAutoMapperProfile : Profile
    {
        public APIAutoMapperProfile()
        {
            CreateMap<LoginReqModel, LoginRequest>()
           .ReverseMap();

            CreateMap<LoginResponseDto, LoginResponse>()
           .ReverseMap();


        }
    }
}
