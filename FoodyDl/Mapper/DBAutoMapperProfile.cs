using AutoMapper;
using FoodyDl.FoodyService.DTOs;
using FoodyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDl.Mapper
{
    public class DBAutoMapperProfile : Profile
    {
        public DBAutoMapperProfile()
        {
            CreateMap<ViewUser, UserDTO>()
             .ReverseMap();
        }

    }
}
