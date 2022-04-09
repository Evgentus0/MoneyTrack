using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetLastTransactions(Paging paging, string userId);
        Task Add(TransactionDto transaction);
        Task<List<TransactionDto>> GetQueryTransactions(DbQueryRequest request, string userId);
        Task<int> CountTransactions(List<Filter> filters = null);
        Task Delete(int id);
        Task Update(TransactionDto transaction);
        Task<decimal> CalculateTotalBalance(List<Filter> filters);
    }
}
