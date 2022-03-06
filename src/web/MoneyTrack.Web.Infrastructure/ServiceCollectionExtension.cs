using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Web.Infrastructure.Interfaces;
using MoneyTrack.Web.Infrastructure.Services;
using MoneyTrack.Web.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Web.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, AppSettings settings)
        {
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddSingleton(settings);
        }
    }
}
