using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.WPF.DomainServices.DbProvider;
using MoneyTrack.WPF.DomainServices.Repositories;
using MoneyTrack.WPF.Infrastructure.Settings;

namespace MoneyTrack.WPF.DomainServices.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped(provider => new LiteDbProvider(provider.GetRequiredService<AppSettings>()));
        }
    }
}
