using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using System.Collections.Generic;

namespace MoneyTrack.Core.AppServices.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public void Add(TransactionDto transaction)
        {
            var entity = _mapper.Map<Transaction>(transaction);
            _transactionRepository.Add(entity);
        }

        public List<TransactionDto> GetLastTransaction(int numberOfLastTransaction)
        {
            return _mapper.Map<List<TransactionDto>>(_transactionRepository.GetLastTransaction(numberOfLastTransaction));
        }
    }
}
