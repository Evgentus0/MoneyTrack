using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Identity
{
    public interface IUserManager
    {
        Task<bool> CheckIsAuthenticate(string login, string password);
        Task<User> GetByLogin(string login);
        Task<User> Create(User user, string password);
    }
}
