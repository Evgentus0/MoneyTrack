using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Exceptions;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly AccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionService(TransactionRepository transactionRepository, AccountRepository accountRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task Add(TransactionDto transaction)
        {
            if (transaction.SetCurrentDttm)
                transaction.AddedDttm = DateTimeOffset.Now;

            var validationError = transaction.GetErrorString();
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new AppValidationException(validationError);
            }

            var newTransaction = _mapper.Map<Transaction>(transaction);

            var lastAccountTransaction = await _transactionRepository.GetLastAccountTransaction(transaction.Account.Id);

            if (lastAccountTransaction.AddedDttm > newTransaction.AddedDttm)
            {
                // Add not last transaction
                var lastBeforeNew = (await _transactionRepository.GetQueriedTransactions(new DbQueryRequest
                {
                    Filters = new List<Filter>
                    {
                       new Filter
                       {
                           PropName = $"{nameof(Transaction.Account)}.{nameof(Transaction.Account.Id)}",
                           Operation = Operations.Eq,
                           Value = newTransaction.Account.Id.ToString(),
                           FilterOp = FilterOp.And
                       },
                       new Filter
                       {
                           PropName = nameof(Transaction.AddedDttm),
                           Operation = Operations.Less,
                           Value = newTransaction.AddedDttm.ToString(),
                           FilterOp = FilterOp.And
                       }
                    },
                    Sorting = new Sorting
                    {
                        Direction = SortDirect.Desc,
                        PropName = nameof(Transaction.AddedDttm)
                    }
                })).FirstOrDefault();

                if(lastBeforeNew != null)
                {
                    newTransaction.AccountRest = lastAccountTransaction.AccountRest + newTransaction.Quantity;
                }
                else
                {
                    newTransaction.AccountRest = newTransaction.Quantity;
                }

                await UpdateTransactionsRestSinceDate(newTransaction.Account.Id, newTransaction.AddedDttm, newTransaction.Quantity);

                await _transactionRepository.Save();

                return;
            }

            newTransaction.AccountRest = lastAccountTransaction.AccountRest + newTransaction.Quantity;
            newTransaction.Id = await _transactionRepository.GetNewAvailableId();

            await _transactionRepository.Add(newTransaction);

            var account = await _accountRepository.GetById(newTransaction.Account.Id);

            account.LastTransaction.Id = newTransaction.Id;

            await _transactionRepository.Save();
        }

        public async Task<List<TransactionDto>> GetLastTransactions(Paging paging, string userId)
        {
            var result = await _transactionRepository.GetQueriedTransactions(new DbQueryRequest
            {
                Paging = paging,
                Sorting = new Sorting
                {
                    Direction = SortDirect.Desc,
                    PropName = nameof(Transaction.AddedDttm)
                },
                Filters = new List<Filter>
                {
                    new Filter
                    {
                        FilterOp = FilterOp.And,
                        Operation = Operations.Eq,
                        PropName = nameof(Transaction.Account)+"."+nameof(Account.User)+nameof(Account.User.Id),
                        Value = userId
                    }
                }
            });

            return _mapper.Map<List<TransactionDto>>(result);
        }

        public async Task<List<TransactionDto>> GetQueryTransactions(DbQueryRequest request, string userId)
        {
            request.Filters = request.Filters ?? new List<Filter>();
            request.Filters.Add(new Filter
            {
                FilterOp = FilterOp.And,
                Operation = Operations.Eq,
                PropName = nameof(Transaction.Account) + "." + nameof(Account.User) + nameof(Account.User.Id),
                Value = userId
            });

            List<Transaction> transactions = await _transactionRepository.GetQueriedTransactions(request);

            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<int> CountTransactions(List<Filter> filters = null)
        {
            return await _transactionRepository.CountTrasactions(filters ?? new List<Filter>());
        }

        public async Task Update(TransactionDto transaction)
        {
            var transactionToUpdate = await _transactionRepository.GetById(transaction.Id);

            if (transactionToUpdate != null)
            {
                if (!string.IsNullOrEmpty(transaction.Description))
                    transactionToUpdate.Description = transaction.Description;

                if (transaction.Category != null && transaction.Category.Id > 0)
                    transactionToUpdate.Category.Id = transaction.Category.Id;

                var isDateTimeChange = false;
                if (transaction.AddedDttm.HasValue && transaction.AddedDttm.Value > Transaction.CutOffDate
                    && transaction.AddedDttm.Value != transactionToUpdate.AddedDttm)
                {
                    isDateTimeChange = true;

                    var oldDate = transactionToUpdate.AddedDttm;
                    var newDate = transaction.AddedDttm.Value;

                    var oldQuantity = transactionToUpdate.Quantity;

                    transactionToUpdate.Quantity = transaction.Quantity.HasValue && transaction.Quantity.Value != transactionToUpdate.Quantity
                        ? transaction.Quantity.Value
                        : transactionToUpdate.Quantity;

                    transactionToUpdate.AddedDttm = newDate;

                    await UpdateTransactionsRestSinceDate(transactionToUpdate.Account.Id, oldDate, -oldQuantity);
                    await _transactionRepository.Save();
                    await UpdateTransactionsRestSinceDate(transactionToUpdate.Account.Id, newDate, transactionToUpdate.Quantity);
                }

                if(transaction.Quantity.HasValue && transaction.Quantity.Value != transactionToUpdate.Quantity && !isDateTimeChange)
                {
                    var diff = transaction.Quantity.Value - transactionToUpdate.Quantity;
                    transactionToUpdate.Quantity = transaction.Quantity.Value;

                    await UpdateTransactionsRestSinceDate(transactionToUpdate.Account.Id, transactionToUpdate.AddedDttm, diff);
                }

                await _transactionRepository.Update(transactionToUpdate);

                await _transactionRepository.Save();
            }
        }

        public async Task Delete(int id)
        {
            var transaction = await _transactionRepository.GetById(id);

            if (transaction is null)
            {
                return;
            }

            await UpdateTransactionsRestSinceDate(transaction.Account.Id, transaction.AddedDttm, -transaction.Quantity);

            await _transactionRepository.Delete(id);

            await _transactionRepository.Save();
        }

        public async Task<decimal> CalculateTotalBalance(List<Filter> filters)
        {
            return await _transactionRepository.CalculateSum(nameof(Transaction.Quantity), filters);
        }

        private async Task UpdateTransactionsRestSinceDate(int accountId, DateTimeOffset date, decimal quantity)
        {
            var transactionsToUpdate = await _transactionRepository.GetQueriedTransactions(new DbQueryRequest
            {
                Filters = new List<Filter>
                {
                    new Filter
                    {
                        PropName = $"{nameof(Transaction.Account)}.{nameof(Transaction.Account.Id)}",
                        Operation = Operations.Eq,
                        Value = accountId.ToString(),
                        FilterOp = FilterOp.And
                    },
                    new Filter
                    {
                        PropName = nameof(Transaction.AddedDttm),
                        Operation = Operations.Greater,
                        Value = date.ToString(),
                        FilterOp = FilterOp.And
                    }
                }
            });

            foreach (var transaction in transactionsToUpdate)
            {
                transaction.AccountRest += quantity;

                await _transactionRepository.Update(transaction);
            }
        }
    }
}
