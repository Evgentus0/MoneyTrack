using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
