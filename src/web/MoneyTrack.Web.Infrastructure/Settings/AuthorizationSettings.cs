using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Web.Infrastructure.Settings
{
    public class AuthorizationSettings
    {
        public string SecretKey { get; set; }
        public double MinutesToExpiration { get; set; }
    }
}
