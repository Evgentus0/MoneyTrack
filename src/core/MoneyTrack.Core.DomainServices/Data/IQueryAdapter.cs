﻿using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface IQueryAdapter<T>
    {
        IQueryAdapter<T> Where(List<Filter> filters);
        IQueryAdapter<T> Where(Filter filter);
        IQueryAdapter<T> OrderByDesc(string propName);
        IQueryAdapter<T> OrderBy(string propName);
        IQueryAdapter<T> Take(int number);
        IQueryAdapter<T> Include(string propName);
        IQueryAdapter<T> Skip(int count);

        Task<T> First();
        Task<List<T>> ToList();
        Task<int> Count();
        Task<NumberType> Sum<NumberType>(string propName);
    }
}
