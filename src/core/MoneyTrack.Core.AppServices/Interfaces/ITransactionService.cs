using MoneyTrack.Core.AppServices.DTOs;
using System.Collections.Generic;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDto> GetLastTransaction(int numberOfLastTransaction);
        void Add(TransactionDto transaction);
    }
}
