﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Identity
{
    public class UserRoles
    {
        public const string User = "User";
        public const string Admin = "Admin";

        private static List<string> _roles;
        public static List<string> GetRoles()
        {
            if(_roles is null)
            {
                _roles = new List<string>
                {
                    User, Admin
                };
            }

            return _roles;
        }
    }
}
