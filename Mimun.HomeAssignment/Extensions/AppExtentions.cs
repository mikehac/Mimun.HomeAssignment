using Microsoft.EntityFrameworkCore;
using Mimun.HomeAssignment.Models;
using Mimun.HomeAssignment.Repository;

namespace Mimun.HomeAssignment.Extensions
{
    public static class AppExtentions
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<MimunDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ShoppingDb"));
            });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
        }
    }
}
