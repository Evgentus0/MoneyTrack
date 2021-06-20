using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.DomainServices.DbProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDbProvider _dbProvider;

        public TransactionRepository(LiteDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public void Add(Transaction transaction)
        {
            using var db = _dbProvider.GetDb();
            var collection = db.GetCollection<Transaction>();

            collection.Insert(transaction);
        }

        public Task<Transaction> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetLastTransaction(int numberOfLastTransaction)
        {
            using var db = _dbProvider.GetDb();
            var collection = db.GetCollection<Transaction>();

            return collection.Query()
                .Include(x => x.Account)
                .Include(x => x.Category)
                .OrderByDescending(x => x.AddedDttm).Limit(numberOfLastTransaction).ToList();

        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
