using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.WPF.DomainServices.LiteDB.DbProvider;
using MoneyTrack.WPF.DomainServices.LiteDB.Repositories;
using MoneyTrack.WPF.Infrastructure.Settings;

namespace MoneyTrack.WPF.DomainServices.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddWPFDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped(provider => new LiteDbProvider(provider.GetRequiredService<AppSettings>()));
        }
    }
}
