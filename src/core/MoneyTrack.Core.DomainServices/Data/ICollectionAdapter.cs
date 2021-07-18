using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface ICollectionAdapter<T>
    {
        Task Add(T item);
        Task Update(T item);
        Task Remove(int id);
        IQueryAdapter<T> Query { get; }
    }
}
