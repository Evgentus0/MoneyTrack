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
        IQueryAdapter<T> Skip(int count);

        Task<T> First();
        Task<List<T>> ToList();
        Task<int> Count();
        Task<decimal> SumDecimal(string propName);
        Task<int> SumInt(string propName);
        Task<double> SumDouble(string propName);
        Task<long> SumLong(string propName);
    }
}
