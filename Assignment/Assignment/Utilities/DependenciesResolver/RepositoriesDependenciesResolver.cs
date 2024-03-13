using Assignment.Domain.Contracts.IRepositories;
using Assignment.Domain.Contracts.Repositories;
using Assignment.Infrastructure.Reposatories;


namespace Assignment.Utilities.DependenciesResolver
{
    public class RepositoriesDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
        }
    }
}
