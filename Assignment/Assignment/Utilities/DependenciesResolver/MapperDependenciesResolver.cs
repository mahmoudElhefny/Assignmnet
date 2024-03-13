using Assignment.Service.Mapper;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace Assignment.Utilities.DependenciesResolver
{
    public class MapperDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            var configuraion = MapperConfigurator.ConfigureMappings();
            services.AddSingleton<IMapper>(new Mapper(configuraion));
        }
    }
}
