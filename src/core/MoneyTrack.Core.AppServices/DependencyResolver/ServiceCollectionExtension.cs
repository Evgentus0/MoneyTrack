using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.AppServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
