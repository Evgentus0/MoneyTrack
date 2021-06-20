using MoneyTrack.Core.Models;
using System.Collections.Generic;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
    }
}
