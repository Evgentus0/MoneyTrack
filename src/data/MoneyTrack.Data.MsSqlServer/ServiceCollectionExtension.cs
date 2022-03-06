using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Data.MsSqlServer.Db;
using MoneyTrack.Web.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTrack.Data.MsSqlServer.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Identity;
using MoneyTrack.Data.MsSqlServer.Identity;

namespace MoneyTrack.Data.MsSqlServer
{
    public static class ServiceCollectionExtension
    {
        public static void AddMsSqlDb(this IServiceCollection services, 
            IConfiguration configuration, ILoggerFactory loggerFactory, AppSettings settings)
        {
            services.AddScoped<IDbProvider, DbProvider>();

            services.AddDbContext<MoneyTrackContext>(options =>
            {
                var optionsBuilder = options.UseLoggerFactory(loggerFactory)
                                            .EnableSensitiveDataLogging();

                switch (settings.DbType)
                {
                    case DbType.MsSqlServer:
                        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MsSqlServerConnection"));
                        break;
                    case DbType.InMemory:
                        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                        break;
                    default:
                        throw new ArgumentException($"Incorrect db type {settings.DbType}");
                }
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredUniqueChars = 6,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = true
                };
            })
                .AddEntityFrameworkStores<MoneyTrackContext>();

            services.AddScoped<IUserManager, AppUserManager>();
        }
    }
}
