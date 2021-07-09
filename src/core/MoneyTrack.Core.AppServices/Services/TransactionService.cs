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
            return _mapper.Map<List<TransactionDto>>(await _transactionRepository.GetLastTransactions(paging));
        }

        public async Task<List<TransactionDto>> GetFilteredTransaction(List<Filter> filters)
        {
            List<Transaction> transactions = await _transactionRepository.GetFilteredTransactions(filters);

            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<int> CountTransactions()
        {
            return await _transactionRepository.CountTrasactions();
        }
    }
}
