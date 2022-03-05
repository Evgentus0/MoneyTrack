using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Data.MsSqlServer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer
{
    public class CollectionAdapter<T, TEntity, IdType> : ICollectionAdapter<T, IdType> where TEntity : class, IEntity<IdType>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public CollectionAdapter(DbSet<TEntity> dbSet, IMapper mapper)
        {
            _dbSet = dbSet;
            _mapper = mapper;
        }

        public IQueryAdapter<T> Query => new QueryAdapter<T, TEntity>(_dbSet.AsQueryable(), _mapper);

        public async Task Add(T item)
        {
            var entity = _mapper.Map<TEntity>(item);

            await _dbSet.AddAsync(entity);
        }

        public async Task Remove(IdType id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

            _dbSet.Remove(entity);
        }

        public async Task Update(T item)
        {
            var entity = _mapper.Map<TEntity>(item);

            _dbSet.Update(entity);
        }
    }
}
