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
        private ObservableCollection<TransactionModel> _lastTransactions;
        private ObservableCollection<AccountModel> _accounts;
        private ObservableCollection<CategoryModel> _categories;
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly AppSettings _settings;
        private readonly IMapper _mapper;

        #endregion

        #region Properties

        public ObservableCollection<CategoryModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public ObservableCollection<AccountModel> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }
        public ObservableCollection<TransactionModel> LastTransactions
        {
            get => _lastTransactions;
            set
            {
                _lastTransactions = value;
                OnPropertyChanged(nameof(LastTransactions));
            }
        }
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
        }

        public override void Initialize()
        {
            NewTransaction = new TransactionModel();

            SetLastTransactions();
            SetCategories();
            SetAccounts();
        }

        private void SetAccounts()
        {
            Accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(_accountService.GetAllAccounts()));
        }

        private void SetCategories()
        {
            Categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(_categoryService.GetAllCategories()));
        }

        private void SetLastTransactions()
        {
            LastTransactions = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(_transactionService.GetLastTransaction(_settings.NumberOfLastTransaction)));
        }

        public RelayCommand AddTransactionCommand
        {
            get
            {
                return _addTransaction ??= new RelayCommand(obj =>
                {
                    var transactionEntity = _mapper.Map<TransactionDto>(NewTransaction);
                    _transactionService.Add(transactionEntity);

                    SetLastTransactions();

                    NewTransaction = new TransactionModel();
                });
            }
        }


    }
}
