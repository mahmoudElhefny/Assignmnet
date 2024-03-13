using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Mapper
{
    public class MapperConfigurator
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(mapperConfigs =>
            {
                UserMapper.ConfigureMapping(mapperConfigs);
                ProductMapper.ConfigureMapping(mapperConfigs);
            });

            return mapperConfiguration;
        }
    }
}
