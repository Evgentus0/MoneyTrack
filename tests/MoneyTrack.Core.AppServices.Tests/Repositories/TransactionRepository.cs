using MoneyTrack.Core.DomainServices.Interfaces;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Tests.Repositories
{
    internal class TransactionRepository : ITransactionRepository
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public int SaveInvokesCount { get; private set; } = 0;

        public Task Add(Transaction transaction)
        {
            Transactions.Add(transaction);
            return Task.CompletedTask;
        }

        public Task<Transaction> AddWithSave(Transaction transaction)
        {
            SaveInvokesCount++;
            Transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public Task<decimal> CalculateSum(string propName, List<Filter> filters)
        {
            if(propName == nameof(Transaction.Quantity))
            {
                return Task.FromResult(Transactions.Sum(x => x.Quantity));
            }

            return Task.FromResult(0m);
        }

        public Task<int> CountTrasactions(List<Filter> filters)
        {
            return Task.FromResult(Transactions.Count);
        }

        public Task Delete(int id)
        {
            Transactions.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<Transaction?> GetById(int id)
        {
            return Task.FromResult(Transactions.FirstOrDefault(x => x.Id == id));
        }

        public Task<Transaction> GetLastAccountTransaction(int accountId)
        {
            return Task.FromResult(Transactions.Where(x => x.Account.Id == accountId)
                .OrderByDescending(x => x.AddedDttm)
                .First());
        }

        public Task<List<Transaction>> GetQueriedTransactions(DbQueryRequest request)
        {
            var res = Transactions.AsEnumerable();

            if(request.Filters is not null)
            {
                var filterProc = FilterProcess(request.Filters.FirstOrDefault());
                res = res.Where(filterProc.func);
                foreach (var filter in request.Filters!.Skip(1))
                {
                    filterProc = FilterProcess(filter);

                    if (filterProc.useAnd)
                    {
                        res = res.Where(filterProc.func);
                    }
                    else
                    {
                        res.Union(Transactions.Where(filterProc.func));
                    }
                }
            }

            if(request.Sorting is not null)
            {
                res = SortProcess(res, request.Sorting);
            }

            if(request.Paging is not null)
            {
                res = res.Skip(request.Paging.PageSize * (request.Paging.CurrentPage - 1)).Take(request.Paging.PageSize);
            }

            return Task.FromResult(res.ToList());
        }

        public Task Save()
        {
            SaveInvokesCount++;
            return Task.CompletedTask;
        }

        public Task Update(Transaction transaction)
        {
            Transactions.RemoveAll(x => x.Id == transaction.Id);
            Transactions.Add(transaction);

            return Task.CompletedTask;
        }

        private (Func<Transaction, bool> func, bool useAnd) FilterProcess(Filter? filter) => filter switch
        {
            {
                PropName: $"{nameof(Transaction.Account)}.{nameof(Transaction.Account.Id)}",
                Operation: Operations.Eq,
            } => (new Func<Transaction, bool>(x => x.Account!.Id == int.Parse(filter.Value!)),
                filter.FilterOp == FilterOp.And),

            {
                PropName: nameof(Transaction.AddedDttm),
                Operation: Operations.Greater
            } => (new Func<Transaction, bool>(x => x.AddedDttm > DateTime.Parse(filter.Value!)),
                filter.FilterOp == FilterOp.And),

            {
                PropName: nameof(Transaction.AddedDttm),
                Operation: Operations.Less
            } => (new Func<Transaction, bool>(x => x.AddedDttm < DateTime.Parse(filter.Value!)),
                filter.FilterOp == FilterOp.And),

            null => (new Func<Transaction, bool>(x => true), true)
        };

        private IEnumerable<Transaction> SortProcess(IEnumerable<Transaction> source,
            Sorting sort) => sort switch
        {
            {
                PropName: nameof(Transaction.AddedDttm)
            } => sort.Direction == SortDirect.Asc
                ? source.OrderBy(x => x.AddedDttm)
                : source.OrderByDescending(x => x.AddedDttm)
        };
    }
}
