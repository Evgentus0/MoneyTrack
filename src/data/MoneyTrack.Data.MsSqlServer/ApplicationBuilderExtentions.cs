using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MoneyTrack.Core.DomainServices.Identity;
using MoneyTrack.Data.MsSqlServer.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder TryCreateDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MoneyTrackContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<IUserManager>();

                DbInitializer.Initialize(context, userManager);
            }

            return app;
        }
    }
}
