using LiteDB;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppSettings _settings;

        public AccountRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public List<Account> GetAllAccounts()
        {
            using var db = new LiteDatabase(_settings.LiteDBConnection);

            var collection = db.GetCollection<Account>();

            return collection.Query().ToList();
        }
    }
}
