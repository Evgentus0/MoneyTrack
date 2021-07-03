using LiteDB;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Data.LiteDB
{
    public class QueryAdapter<T>:IQueryAdapter<T>
    {
        private readonly ILiteQueryable<T> _queryable;

        public QueryAdapter(ILiteQueryable<T> queryable)
        {
            _queryable = queryable;
        }

        public IQueryAdapter<T> OrderBy(string propName)
        {
            return new QueryAdapter<T>(_queryable.OrderBy(BsonExpression.Create(propName)));
        }


        public IQueryAdapter<T> OrderByDesc(string propName)
        {
            return new QueryAdapter<T>(_queryable.OrderByDescending(BsonExpression.Create(propName)));
        }

        public IQueryAdapter<T> Take(int number)
        {
            return new QueryAdapter<T>((ILiteQueryable<T>)_queryable.Limit(number));
        }

        public async Task<List<T>> ToList()
        {
            return await Task.Run(() => _queryable.ToList());
        }

        public async Task<T> First()
        {
            return await Task.Run(() => _queryable.FirstOrDefault());
        }

        public IQueryAdapter<T> Where(Filter filter)
        {
            return new QueryAdapter<T>(_queryable.Where(BsonExpression.Create(filter.ToBsonExpression())));
        }

        public IQueryAdapter<T> Include(string propName)
        {
            return new QueryAdapter<T>(_queryable.Include(BsonExpression.Create(propName)));
        }
    }
}
