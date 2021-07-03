using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface IDbProvider: IDisposable
    {
        ICollectionAdapter<Transaction> Transactions { get; }
        ICollectionAdapter<Category> Categories { get; }
        ICollectionAdapter<Account> Accounts { get; }
    }
}
