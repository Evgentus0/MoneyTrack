using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer.Entites
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
