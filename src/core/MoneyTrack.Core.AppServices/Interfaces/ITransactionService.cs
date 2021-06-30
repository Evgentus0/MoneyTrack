using MoneyTrack.Core.AppServices.DTOs;
using System;
using System.Collections.Generic;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDto> GetLastTransaction(int numberOfLastTransaction);
        void Add(TransactionDto transaction);
        List<TransactionDto> GetTransactionFromTo(DateTimeOffset from, DateTimeOffset to);
    }
}
