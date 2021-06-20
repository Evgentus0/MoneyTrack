using MoneyTrack.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);

        Task<Transaction> GetById(int id);

        Task Update(Transaction transaction);
        Task Remove(int id);
        List<Transaction> GetLastTransaction(int numberOfLastTransaction);
    }
}
