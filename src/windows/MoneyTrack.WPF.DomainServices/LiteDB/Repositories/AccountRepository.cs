using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.DomainServices.LiteDB.DbProvider;
using System.Collections.Generic;

namespace MoneyTrack.WPF.DomainServices.LiteDB.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LiteDbProvider _dbProvider;

        public AccountRepository(LiteDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public List<Account> GetAllAccounts()
        {
            using var db = _dbProvider.GetDb();

            var collection = db.GetCollection<Account>();

            return collection.Query().ToList();
        }
    }
}
