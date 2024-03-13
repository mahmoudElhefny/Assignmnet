using Assignment.Infrastructure.Entities;
using Assignment.Service.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Mapper
{
    public static class UserMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<RegisterViewModel, ApplicationUser>();         
        }
    }
}
