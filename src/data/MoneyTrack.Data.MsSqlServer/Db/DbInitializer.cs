using Microsoft.AspNetCore.Identity;
using MoneyTrack.Core.DomainServices.Identity;
using MoneyTrack.Core.Models;
using MoneyTrack.Data.MsSqlServer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer.Db
{
    internal class DbInitializer
    {
        internal static void Initialize(MoneyTrackContext context, IUserManager userManager)
        {
            context.Database.EnsureCreated();

            var isInit = userManager.GetByLogin("zerom2016romanenko@gmail.com").Result is not null;

            if (isInit)
            {
                return;
            }

            #region Roles
            foreach (var role in UserRoles.GetRoles())
            {
                userManager.CreateRole(role).Wait();
            }

            context.SaveChanges();
            #endregion

            #region Users
            var user = new User
            {
                FirstName = "Eugene",
                LastName = "Romanenko",
                Email = "zerom2016romanenko@gmail.com",
                UserName = "zerom2016romanenko@gmail.com",
                Roles = new List<string> { UserRoles.Admin }
            };

            userManager.CreateUser(user, "Password_1").Wait();

            context.SaveChanges();
            #endregion
        }
    }
}
