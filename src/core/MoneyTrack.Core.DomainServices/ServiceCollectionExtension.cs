using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Repositories;

namespace MoneyTrack.Core.DomainServices
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<AccountRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<TransactionRepository>();
        }
    }
}
