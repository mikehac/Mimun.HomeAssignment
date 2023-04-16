using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
            services.AddScoped<IPackageRepository, PackageRepository>();
        }

        public static async Task<T>? GetFromCache<T>(this IMemoryCache memoryCache, string cacheKey, Func<Task<T>> func)
        {
            if (!memoryCache.TryGetValue(cacheKey, out T? response))
            {
                response = await func();
                if (response != null)
                {
                    MemoryCacheEntryOptions cacheEntryOptions = CreateCacheOptions();
                    memoryCache.Set(cacheKey, response, cacheEntryOptions);
                }
            }

            return response;
        }
        private static MemoryCacheEntryOptions CreateCacheOptions()
        {
            return new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);
        }
    }
}
