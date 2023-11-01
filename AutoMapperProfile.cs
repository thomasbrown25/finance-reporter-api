using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using finance_reporter_api.Models;
using finance_reporter_api.Dtos.User;

namespace financing_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoadUserDto>();
        }
    }
}
