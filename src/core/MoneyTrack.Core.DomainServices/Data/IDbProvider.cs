using MoneyTrack.Core.Models;
using System;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface IDbProvider: IDisposable
    {
        ICollectionAdapter<Transaction> Transactions { get; }
        ICollectionAdapter<Category> Categories { get; }
        ICollectionAdapter<Account> Accounts { get; }
    }
}
