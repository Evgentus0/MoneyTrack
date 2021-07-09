using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetLastTransactions(Paging paging);
        Task Add(TransactionDto transaction);
        Task<List<TransactionDto>> GetFilteredTransaction(List<Filter> filters);
        Task<int> CountTransactions();
    }
}
