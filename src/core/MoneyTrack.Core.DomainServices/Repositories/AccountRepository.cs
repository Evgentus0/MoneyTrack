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
    }
}
