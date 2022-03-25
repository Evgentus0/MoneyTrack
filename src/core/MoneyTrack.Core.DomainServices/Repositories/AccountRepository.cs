using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public class AccountRepository
    {
        private readonly IDbProvider _dbProvider;

        public AccountRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<List<Account>> GetAccounts(List<Filter> filters)
        {
            var accounts = _dbProvider.Accounts.Query;

            if (filters != null && filters.Any())
            {
                accounts = accounts.Where(filters);
            }

            return await accounts.ToList();
        }

        public async Task Add(Account account)
        {
            await _dbProvider.Accounts.Add(account);
        }

        public async Task Delete(int id)
        {
            await _dbProvider.Accounts.Remove(id);
        }

        public async Task Update(Account account)
        {
            await _dbProvider.Accounts.Update(account);
        }

        public async Task<Account> GetById(int id)
        {
            return await _dbProvider.Accounts.Query.Where(new Models.Operational.Filter
            {
                PropName = nameof(Account.Id),
                Operation = Models.Operational.Operations.Eq,
                Value = id.ToString()
            }).First();
        }

        public async Task Save()
        {
            await _dbProvider.Save();
        }
    }
}
