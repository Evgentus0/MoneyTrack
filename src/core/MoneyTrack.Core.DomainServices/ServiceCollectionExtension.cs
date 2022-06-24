using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;

namespace MoneyTrack.Core.DomainServices
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
