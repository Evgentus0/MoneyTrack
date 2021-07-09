using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public class TransactionRepository
    {
        private readonly IDbProvider _dbProvider;

        public TransactionRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task Add(Transaction transaction)
        {
            await _dbProvider.Transactions.Add(transaction);
        }

        public async Task<Transaction> GetById(int id)
        {
            var filter = new Filter
            {
                Operations = Operations.Eq,
                PropName = nameof(Transaction.Id),
                Value = id.ToString()
            };

            var result = _dbProvider.Transactions.Query.Where(filter);
            if(result is null)
            {
                throw new ArgumentException($"Transaction with id {id} is not exist");
            }
            return await result.First();
        }

        public async Task Update(Transaction transaction)
        {
            await _dbProvider.Transactions.Update(transaction);
        }
        public async Task Remove(int id)
        {
            await _dbProvider.Transactions.Remove(id);
        }
        public async Task<List<Transaction>> GetLastTransactions(Paging paging)
        {
            var result = _dbProvider.Transactions.Query
                .Include(nameof(Account))
                .Include(nameof(Category))
                .OrderByDesc(nameof(Transaction.AddedDttm))
                .Skip(paging.PageSize * (paging.CurrentPage - 1));
            return  await result.Take(paging.PageSize).ToList();
        }

        public async Task<List<Transaction>> GetFilteredTransactions(List<Filter> filters)
        {
            var result = _dbProvider.Transactions.Query
                .Include(nameof(Account))
                .Include(nameof(Category));

            foreach(var filter in filters)
            {
                result = result.Where(filter);
            }

            return await result.ToList();
        }

        public async Task<int> CountTrasactions()
        {
            return await _dbProvider.Transactions.Query.Count();
        }
    }
}
