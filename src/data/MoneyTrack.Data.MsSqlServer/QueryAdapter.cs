using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.DomainServices.Exceptions;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using static MoneyTrack.Data.MsSqlServer.ExpressionHelper;

namespace MoneyTrack.Data.MsSqlServer
{
    public class QueryAdapter<T, TEntity> : IQueryAdapter<T> where TEntity : class
    {
        private IQueryable<TEntity> _query;
        private readonly IMapper _mapper;

        public QueryAdapter(IQueryable<TEntity> query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        public async Task<int> Count()
        {
            return await _query.CountAsync();
        }

        public async Task<T> First()
        {
            var entity =  await _query.AsNoTracking().FirstOrDefaultAsync();

            return _mapper.Map<T>(entity);
        }

        public IQueryAdapter<T> Include(string propName)
        {
            _query = _query.Include(propName);

            return this;
        }

        public IQueryAdapter<T> OrderBy(string propName)
        {
            _query = _query.OrderBy(ToLambda<TEntity>(propName));

            return this;
        }

        public IQueryAdapter<T> OrderByDesc(string propName)
        {
            _query = _query.OrderByDescending(ToLambda<TEntity>(propName));

            return this;
        }

        public IQueryAdapter<T> Skip(int count)
        {
            _query = _query.Skip(count);

            return this;
        }

        public async Task<NumberType> Sum<NumberType>(string propName)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(TEntity)).Find(propName, true);

            if(typeof(NumberType) == typeof(int))
            {
                return (NumberType)Convert.ChangeType(await _query.SumAsync(x => (int)prop.GetValue(x)), typeof(NumberType));
            }

            if (typeof(NumberType) == typeof(long))
            {
                return (NumberType)Convert.ChangeType(await _query.SumAsync(x => (long)prop.GetValue(x)), typeof(NumberType));
            }

            if (typeof(NumberType) == typeof(double))
            {
                return (NumberType)Convert.ChangeType(await _query.SumAsync(x => (double)prop.GetValue(x)), typeof(NumberType));
            }

            if (typeof(NumberType) == typeof(decimal))
            {
                return (NumberType)Convert.ChangeType(await _query.SumAsync(x => (decimal)prop.GetValue(x)), typeof(NumberType));
            }

            throw new MoneyTrackException($"{typeof(NumberType).AssemblyQualifiedName} is not appropriate type for Sum method");
        }

        public IQueryAdapter<T> Take(int number)
        {
            _query = _query.Take(number);

            return this;
        }

        public async Task<List<T>> ToList()
        {
            var entities = await _query.AsNoTracking().ToListAsync();

            return _mapper.Map<List<T>>(entities);
        }

        public IQueryAdapter<T> Where(List<Filter> filters)
        {
            _query = _query.Where(ExpressionFromFilters<TEntity>(filters));

            return this;
        }

        public IQueryAdapter<T> Where(Filter filter)
        {
            _query = _query.Where(ExpressionFromFilter<TEntity>(filter));

            return this;
        }
    }
}
