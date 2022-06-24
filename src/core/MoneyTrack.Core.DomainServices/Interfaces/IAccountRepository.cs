using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts(List<Filter> filters);

        Task Add(Account account);

        Task Delete(int id);

        Task Update(Account account);

        Task<Account?> GetById(int id);

        Task Save();
    }
}
