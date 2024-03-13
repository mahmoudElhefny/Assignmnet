using Assignment.Domain.Contracts.IServices;
using Assignment.Infrastructure.__AppContext;
using Assignment.Infrastructure.Entities;
using Assignment.Service.Helpers;
using Assignment.Service.Services;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Utilities.DependenciesResolver
{
    public class ServicesDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();

           // services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
