using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(TransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task Add(TransactionDto transaction)
        {
            if (transaction.SetCurrentDttm)
                transaction.AddedDttm = DateTimeOffset.Now;

            var validationError = transaction.GetErrorString();
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new ValidationException(validationError);
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
            
            if(transactionToUpdate is not null)
            {
                if (transaction.Quantity.HasValue)
                    transactionToUpdate.Quantity = transaction.Quantity.Value;

                if (!string.IsNullOrEmpty(transaction.Description))
                    transactionToUpdate.Description = transaction.Description;

                if (transaction.Category is not null && transaction.Category.Id > 0)
                    transactionToUpdate.Category.Id = transaction.Category.Id;

                if (transaction.Account is not null && transaction.Account.Id > 0)
                    transactionToUpdate.Account.Id = transaction.Account.Id;

                if (transaction.AddedDttm.HasValue && transaction.AddedDttm.Value > Transaction.CutOffDate)
                    transactionToUpdate.AddedDttm = transaction.AddedDttm.Value;

                await _transactionRepository.Update(transactionToUpdate);
            }
        }

        public async Task Delete(int id)
        {
            await _transactionRepository.Remove(id);
        }

        public async Task<decimal> CalculateTotalBalance(List<Filter> filters)
        {
            return await _transactionRepository.CalculateSum(nameof(Transaction.Quantity), filters);
        }
    }
}
