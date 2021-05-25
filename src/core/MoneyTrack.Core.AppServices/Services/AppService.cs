using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class AppService : IAppService
    {
        public Task<List<string>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetLastTransactions(int count)
        {
            throw new NotImplementedException();
        }
    }
}
