using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Client.Commands;
using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields

        private TransactionModel _newTransaction;
        private RelayCommand _addTransaction;
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly AppSettings _settings;
        private readonly IMapper _mapper;

        #endregion

        #region Properties

        public ObservableCollection<CategoryModel> Categories { get; set; }
        public ObservableCollection<AccountModel> Accounts { get; set; }
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

        #endregion


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

            LastTransactions = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(_transactionService.GetLastTransaction(_settings.NumberOfLastTransaction)));

            NewTransaction = new TransactionModel();

            Categories = new ObservableCollection<CategoryModel>
                (_mapper.Map<List<CategoryModel>>(_categoryService.GetAllCategories()));

            Accounts = new ObservableCollection<AccountModel>
                (_mapper.Map<List<AccountModel>>(_accountService.GetAllAccounts()));
            //LastTransactions.CollectionChanged += LastTransactions_CollectionChanged;
        }

        private void LastTransactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LastTransactions));
        }

        public override void Initialize()
        {

        }

        public RelayCommand AddTransactionCommand
        {
            get
            {
                return _addTransaction ??= new RelayCommand(obj =>
                {
                    var transactionEntity = _mapper.Map<TransactionDto>(NewTransaction);
                    _transactionService.Add(transactionEntity);
                });
            }
        }


    }
}
