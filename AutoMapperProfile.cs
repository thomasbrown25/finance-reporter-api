using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using finance_reporter_api.Models;
using finance_reporter_api.Dtos.User;
using finance_reporter_api.Dtos.User;

namespace finance_reporter_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoadUserDto>();
            CreateMap<UserSettings, SettingsDto>();
            CreateMap<SaveSettingsDto, UserSettings>();
        }
    }
}
