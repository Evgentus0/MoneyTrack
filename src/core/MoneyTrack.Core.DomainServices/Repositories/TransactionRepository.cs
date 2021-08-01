using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Threading;
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
                Operation = Operations.Eq,
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
            var transactionToUpdate = await _dbProvider.Transactions.Query.Where(new Filter
            {
                PropName = nameof(transaction.Id),
                Operation = Operations.Eq,
                Value = transaction.Id.ToString()

            }).First();

            if(transactionToUpdate is not null)
            {
                if (transaction.Quantity != 0)
                    transactionToUpdate.Quantity = transaction.Quantity;

                if (!string.IsNullOrEmpty(transaction.Description))
                    transactionToUpdate.Description = transaction.Description;

                if (transaction.Category is not null && transaction.Category.Id > 0)
                    transactionToUpdate.Category.Id = transaction.Category.Id;

                if (transaction.Account is not null && transaction.Account.Id > 0)
                    transactionToUpdate.Account.Id = transaction.Account.Id;

                if (transaction.AddedDttm > Transaction.CutOffDate)
                    transactionToUpdate.AddedDttm = transaction.AddedDttm;

                await _dbProvider.Transactions.Update(transactionToUpdate);
            }

        }
        public async Task Remove(int id)
        {
            await _dbProvider.Transactions.Remove(id);
        }

        public async Task<List<Transaction>> GetQueriedTransactiones(DbQueryRequest request)
        {
            var result = _dbProvider.Transactions.Query
                .Include(nameof(Account))
                .Include(nameof(Category));

            if(request.Filters is not null)
            {
                foreach (var filter in request.Filters)
                {
                    result = result.Where(filter);
                }
            }

            if(request.Sorting is not null)
            {
                switch (request.Sorting.Direction)
                {
                    case SortDirect.Asc:
                        result = result.OrderBy(request.Sorting.PropName);
                        break;
                    case SortDirect.Desc:
                        result = result.OrderByDesc(request.Sorting.PropName);
                        break;
                    default:
                        break;
                }
            }

            if(request.Paging is not null)
            {
                result = result.Skip(request.Paging.PageSize * (request.Paging.CurrentPage - 1)).Take(request.Paging.PageSize);
            }

            return await result.ToList();
        }

        public async Task<decimal> CalculateSum(string propName, List<Filter> filters)
        {
            var result = _dbProvider.Transactions.Query;

            foreach (var filter in filters)
            {
                result = result.Where(filter);
            }

            return await result.Sum(propName);
        }

        public async Task<int> CountTrasactions(List<Filter> filters)
        {
            var result = _dbProvider.Transactions.Query;

            foreach (var filter in filters)
            {
                result = result.Where(filter);
            }

            return await result.Count();
        }
    }
}
