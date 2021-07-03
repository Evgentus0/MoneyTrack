using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface IQueryAdapter<T>
    {
        IQueryAdapter<T> Where(Filter filter);
        IQueryAdapter<T> OrderByDesc(string propName);
        IQueryAdapter<T> OrderBy(string propName);
        IQueryAdapter<T> Take(int number);
        IQueryAdapter<T> Include(string propName);
        Task<T> First();
        Task<List<T>> ToList();
    }
}
