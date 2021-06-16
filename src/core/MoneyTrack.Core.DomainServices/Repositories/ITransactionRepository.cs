using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public interface ITransactionRepository
    {
        Task Add(Transaction transaction);

        Task<Transaction> GetById(int id);

        Task Update(Transaction transaction);
        Task Remove(int id);
        List<Transaction> GetLastTransaction(int numberOfLastTransaction);
    }
}
