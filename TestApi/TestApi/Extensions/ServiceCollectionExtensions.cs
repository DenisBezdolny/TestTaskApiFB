using Microsoft.EntityFrameworkCore;
using TestApi.Domain.Interfaces.Repositories;
using TestApi.Infrastructure;
using TestApi.Infrastructure.Repositories;

namespace TestApi.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Registration Of DbContext
            services.AddDbContext<TestApiDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Registration of Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
