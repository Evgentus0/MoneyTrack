using MoneyTrack.Core.Models;
using System;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface IDbProvider: IDisposable
    {
        ICollectionAdapter<Transaction, int> Transactions { get; }
        ICollectionAdapter<Category, int> Categories { get; }
        ICollectionAdapter<Account, int> Accounts { get; }

        Task Save();
    }
}
