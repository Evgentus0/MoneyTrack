using AutoMapper;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class TransactionListViewModel : BaseViewModel
    {
        private ObservableCollection<TransactionModel> _transactionList;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public ObservableCollection<TransactionModel> TransactionList
        {
            get => _transactionList;
            set
            {
                _transactionList = value;
                OnPropertyChanged(nameof(TransactionList));
            }
        }

        public TransactionListViewModel(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public override string this[string columnName] => string.Empty;

        public override async Task Initialize()
        {
            TransactionList = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetLastTransaction(20)));
        }
    }
}
