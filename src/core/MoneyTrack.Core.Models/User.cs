using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrack.Core.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public List<string>? Roles { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is User user)
            {
                var isRolesEqual = Roles is null && user.Roles is null
                    || (Roles is not null && user.Roles is not null
                    && !Roles.Except(user.Roles).ToList().Any() 
                    && !user.Roles.Except(Roles).ToList().Any());

                return Id is not null ? Id.Equals(user.Id) : Id == user.Id
                    && FirstName is not null ? FirstName.Equals(user.FirstName) : FirstName == user.FirstName
                    && LastName is not null ? LastName.Equals(user.LastName) : LastName == user.LastName
                    && Email is not null ? Email.Equals(user.Email) : LastName == user.Email
                    && UserName is not null ? UserName.Equals(user.UserName) : LastName == user.UserName
                    && isRolesEqual;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Id is not null ? Id.GetHashCode() : 0)
                ^ (FirstName is not null ? FirstName.GetHashCode() : 0)
                ^ (LastName is not null ? LastName.GetHashCode() : 0)
                ^ (Email is not null ? Email.GetHashCode() : 0)
                ^ (UserName is not null ? UserName.GetHashCode() : 0)
                ^ (Roles is not null 
                    ? Roles.Select(x => x is not null ? x.GetHashCode() : 0)
                        .Aggregate((x, y) => x^y) 
                    : 0);
        }
    }
}
