using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mimun.HomeAssignment.Models;
using Mimun.HomeAssignment.Repository;
using Mimun.HomeAssignment.Services;
using System.Text;

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


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "WebAPI",
                    Description = "Product WebAPI"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List < string > ()
                    }
                });
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                };
            });
        }

        // This extention method is build in case we have more services in the future
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
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
