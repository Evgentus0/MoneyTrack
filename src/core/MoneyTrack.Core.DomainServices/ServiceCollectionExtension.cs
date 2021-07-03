using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<AccountRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<TransactionRepository>();

            services.AddScoped<UnitOfWork>();
        }
    }
}
