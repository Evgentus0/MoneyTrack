using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Client.Commands;
using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields

        private TransactionModel _newTransaction;
        private AsyncCommand _addTransaction;
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
            IMapper mapper,
            
            TransactionListViewModel transactionListViewModel)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _accountService = accountService;
            _settings = settings;
            _mapper = mapper;
            TransactionListViewModel = transactionListViewModel;
        }

        public async override Task Initialize()
        {
            NewTransaction = TransactionModel.GetWithDefaultValue();

            await SetLastTransactions();
            await SetCategories();
            await SetAccounts();
        }

        private async Task SetAccounts()
        {
            Accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(await _accountService.GetAllAccounts()));
        }

        private async Task SetCategories()
        {
            Categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(await _categoryService.GetAllCategories()));
        }

        private async Task SetLastTransactions()
        {
            LastTransactions = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetLastTransaction(_settings.NumberOfLastTransaction)));
        }

        public AsyncCommand AddTransactionCommand
        {
            get
            {
                return _addTransaction ??= new AsyncCommand(async obj =>
                {
                    var transactionEntity = _mapper.Map<TransactionDto>(NewTransaction);
                    await _transactionService.Add(transactionEntity);

                    await SetLastTransactions();

                    ResetCurrentTransaction();
                });
            }
        }

        public TransactionListViewModel TransactionListViewModel { get; }

        public override string this[string columnName] => string.Empty;

        private void ResetCurrentTransaction()
        {
            NewTransaction.Quantity = null;
            NewTransaction.Description = null;
        }
    }
}
