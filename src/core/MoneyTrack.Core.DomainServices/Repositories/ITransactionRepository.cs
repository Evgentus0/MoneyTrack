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

        Task<Transaction> GetById(Guid id);

        Task Update(Transaction transaction);
        Task Remove(Guid id);
    }
}
