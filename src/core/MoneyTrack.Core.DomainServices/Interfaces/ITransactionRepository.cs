using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Interfaces
{
    public interface ITransactionRepository
    {
        Task Add(Transaction transaction);

        Task<Transaction> AddWithSave(Transaction transaction);

        Task<Transaction?> GetById(int id);

        Task Update(Transaction transaction);

        Task Delete(int id);

        Task<List<Transaction>> GetQueriedTransactions(DbQueryRequest request);

        Task<Transaction> GetLastAccountTransaction(int accountId);

        Task<decimal> CalculateSum(string propName, List<Filter> filters);

        Task<int> CountTrasactions(List<Filter> filters);

        Task Save();
    }
}
