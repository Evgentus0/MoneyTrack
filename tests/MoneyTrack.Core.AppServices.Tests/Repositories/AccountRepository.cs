using MoneyTrack.Core.DomainServices.Interfaces;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Tests.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        public List<Account> Accounts { get; set; } = new List<Account>();

        public int SaveInvokesCount { get; private set; } = 0;

        public Task Add(Account account)
        {
            Accounts.Add(account);
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            Accounts.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Account>> GetAccounts(List<Filter> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetById(int id)
        {
            return Task.FromResult(Accounts.FirstOrDefault(x => x.Id == id));
        }

        public Task Save()
        {
            SaveInvokesCount++;
            return Task.CompletedTask;
        }

        public Task Update(Account account)
        {
            Accounts.RemoveAll(x => x.Id == account.Id);
            Accounts.Add(account);

            return Task.CompletedTask;
        }
    }
}
