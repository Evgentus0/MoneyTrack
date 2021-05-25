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
        Task Add(TransactionModel transaction);

        Task<TransactionModel> GetById(Guid id);

        Task Update(TransactionModel transaction);
        Task Remove(Guid id);
    }
}
