using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Web.Infrastructure.Settings
{
    public class AppSettings
    {
        public AuthorizationSettings Authorization { get; set; }
        public DbType DbType { get; set; }
        public bool UseSwagger { get; set; } = false;
    }
}
