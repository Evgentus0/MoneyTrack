using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Data.LiteDB
{
    public static class ServiceCollectionExtension
    {
        public static void AddLiteDb(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbProvider, DbProvider>(x => new DbProvider(connectionString));
        }
    }
}
