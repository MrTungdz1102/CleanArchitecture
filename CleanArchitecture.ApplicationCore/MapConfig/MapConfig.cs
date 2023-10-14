using AutoMapper;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.MapConfig
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
          //  CreateMap<AppUser, RegisterRequestDTO>().ReverseMap();
        }
    }
}
