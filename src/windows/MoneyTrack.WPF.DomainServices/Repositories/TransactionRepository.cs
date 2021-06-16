using LiteDB;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppSettings _settings;

        public TransactionRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public Task Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetLastTransaction(int numberOfLastTransaction)
        {
            using var db = new LiteDatabase(_settings.LiteDBConnection);

            var collection = db.GetCollection<Transaction>();

            return collection.Query().OrderByDescending(x => x.AddedDttm)
                .ToEnumerable().Take(numberOfLastTransaction).ToList();
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
