using AutoMapper;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private TransactionModel _newTransaction;
        private bool _setCurrentDttm;
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly AppSettings _settings;
        private readonly IMapper _mapper;

        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<string> Accounts { get; set; }
        public ObservableCollection<TransactionModel> LastTransactions { get; set; }
        public TransactionModel NewTransaction
        {
            get { return _newTransaction; }
            set
            {
                _newTransaction = value;
                OnPropertyChanged(nameof(NewTransaction));
            }
        }
        public bool SetCurrentDttm
        {
            get => _setCurrentDttm;
            set
            {
                _setCurrentDttm = value;
                OnPropertyChanged(nameof(SetCurrentDttm));
            }
        }

        public HomeViewModel(
            ITransactionService transactionService,
            ICategoryService categoryService,
            IAccountService accountService,
            AppSettings settings, 
            IMapper mapper)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _accountService = accountService;
            _settings = settings;
            _mapper = mapper;

            LastTransactions =new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(_transactionService.GetLastTransaction(_settings.NumberOfLastTransaction)));

            Categories = new ObservableCollection<string>(_categoryService.GetAllCategories().Select(x => x.Name).ToList());
            Accounts = new ObservableCollection<string>(_accountService.GetAllAccounts().Select(x => x.Name).ToList());
            //LastTransactions.CollectionChanged += LastTransactions_CollectionChanged;
        }

        private void LastTransactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LastTransactions));
        }

        public override void Initialize()
        {

        }
    }    
}
