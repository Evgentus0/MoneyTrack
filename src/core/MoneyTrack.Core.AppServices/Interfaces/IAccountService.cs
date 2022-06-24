using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAccounts(string userId, List<Filter> filters = null);

        Task AddAccount(AccountDto account);
        Task Update(AccountDto accountDto, bool addTransaction = false);
        Task Delete(int id);
    }
}
