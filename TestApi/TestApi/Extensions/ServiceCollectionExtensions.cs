using Microsoft.EntityFrameworkCore;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.Interfaces.Repositories;
using TestApi.Infrastructure;
using TestApi.Infrastructure.Repositories;
using TestApi.Bll.Services;

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

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IProviderService, ProviderService>();

            return services;
        }
    }
}
