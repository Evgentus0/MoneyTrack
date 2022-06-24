using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Data.MsSqlServer.Db;
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
        private readonly MoneyTrackContext _context;

        public CollectionAdapter(DbSet<TEntity> dbSet, IMapper mapper, MoneyTrackContext context)
        {
            _dbSet = dbSet;
            _mapper = mapper;
            _context = context;
        }

        public IQueryAdapter<T> Query => new QueryAdapter<T, TEntity>(_dbSet.AsQueryable(), _mapper);

        public async Task Add(T item)
        {
            var entity = _mapper.Map<TEntity>(item);

            await _dbSet.AddAsync(entity);
        }

        public async Task<T> AddWithSave(T item)
        {
            var entity = _mapper.Map<TEntity>(item);

            await _dbSet.AddAsync(entity);
            
            await _context.SaveChangesAsync();

            return _mapper.Map<T>(entity);
        }

        public Task ClearLocal()
        {
            foreach (var entity in _dbSet.Local)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return Task.CompletedTask;
        }

        public async Task Remove(IdType id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

            _dbSet.Remove(entity);
        }

        public Task Update(T item)
        {
            var entity = _mapper.Map<TEntity>(item);

            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }
    }
}
