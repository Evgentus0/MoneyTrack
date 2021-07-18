using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _dbProvider.Accounts.Query.ToList();
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
            var existingAccount = await _dbProvider.Accounts.Query.Where(new Models.Operational.Filter
            {
                PropName = nameof(account.Id),
                Operation = Models.Operational.Operations.Eq,
                Value = account.Id.ToString()
            }).First();

            if(existingAccount is not null)
            {
                if (!string.IsNullOrEmpty(account.Name))
                {
                    existingAccount.Name = account.Name;
                }

                await _dbProvider.Accounts.Update(existingAccount);
            }
        }
    }
}
