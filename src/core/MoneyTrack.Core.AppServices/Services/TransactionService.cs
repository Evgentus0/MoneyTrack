using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Exceptions;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
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

            if (!transaction.IsPostponed)
            {
                var account = await _accountRepository.GetById(transaction.Account.Id);
                account.Balance += transaction.Quantity.Value;
                await _accountRepository.Update(account);
            }

            var entity = _mapper.Map<Transaction>(transaction);
            await  _transactionRepository.Add(entity);
        }

        public async Task<List<TransactionDto>> GetLastTransactions(Paging paging)
        {
            var result = await _transactionRepository.GetQueriedTransactiones(new DbQueryRequest
            {
                Paging = paging,
                Sorting = new Sorting
                {
                    Direction = SortDirect.Desc,
                    PropName = nameof(Transaction.AddedDttm)
                }
            });

            return _mapper.Map<List<TransactionDto>>(result);
        }

        public async Task<List<TransactionDto>> GetQueryTransactions(DbQueryRequest request)
        {
            List<Transaction> transactions = await _transactionRepository.GetQueriedTransactiones(request);

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
                if (transaction.Account != null && transaction.Account.Id > 0 && transaction.Account.Id != transactionToUpdate.Account.Id)
                {
                    var transactionQuantity = transactionToUpdate.Quantity;

                    var fromAcc = await _accountRepository.GetById(transactionToUpdate.Account.Id);
                    var toAcc = await _accountRepository.GetById(transaction.Account.Id);

                    fromAcc.Balance -= transactionQuantity;
                    toAcc.Balance += transactionQuantity;

                    await _accountRepository.Update(fromAcc);
                    await _accountRepository.Update(toAcc);

                    transactionToUpdate.Account.Id = transaction.Account.Id;
                }

                if (transaction.Quantity.HasValue && transaction.Quantity.Value != transactionToUpdate.Quantity)
                {
                    var diff = transaction.Quantity.Value - transactionToUpdate.Quantity;
                    transactionToUpdate.Quantity = transaction.Quantity.Value;

                    var acc = await _accountRepository.GetById(transaction.Account.Id);
                    acc.Balance += diff;
                    await _accountRepository.Update(acc);
                }

                if (!string.IsNullOrEmpty(transaction.Description))
                    transactionToUpdate.Description = transaction.Description;

                if (transaction.Category != null && transaction.Category.Id > 0)
                    transactionToUpdate.Category.Id = transaction.Category.Id;

                if (transaction.AddedDttm.HasValue && transaction.AddedDttm.Value > Transaction.CutOffDate)
                    transactionToUpdate.AddedDttm = transaction.AddedDttm.Value;

                await _transactionRepository.Update(transactionToUpdate);
            }
        }

        public async Task Delete(int id)
        {
            var transaction = await _transactionRepository.GetById(id);

            var account = await _accountRepository.GetById(transaction.Account.Id);
            account.Balance -= transaction.Quantity;
            await _accountRepository.Update(account);

            await _transactionRepository.Remove(id);
        }

        public async Task<decimal> CalculateTotalBalance(List<Filter> filters)
        {
            return await _transactionRepository.CalculateSum(nameof(Transaction.Quantity), filters);
        }

        public async Task ApprovePostponedTransaction(int id)
        {
            var transaction = await _transactionRepository.GetById(id);

            if(transaction != null)
            {
                if (transaction.IsPostponed)
                {
                    var account = await _accountRepository.GetById(transaction.Account.Id);
                    account.Balance += transaction.Quantity;
                    await _accountRepository.Update(account);

                    transaction.IsPostponed = false;
                    await _transactionRepository.Update(transaction);
                }
            }
        }
    }
}
