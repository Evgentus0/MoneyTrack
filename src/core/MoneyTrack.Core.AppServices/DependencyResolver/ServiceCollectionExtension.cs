using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.AppServices.Services;

namespace MoneyTrack.Core.AppServices.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
