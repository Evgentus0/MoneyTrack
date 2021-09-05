using AutoMapper;
using MoneyTrack.Clients.Common.Commands;
using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Clients.Common.Settings;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields

        private TransactionModel _newTransaction;
        private AsyncCommand _addTransaction;
        private ObservableCollection<AccountModel> _accounts;
        private ObservableCollection<CategoryModel> _categories;
        private PagingViewModel _paging;
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
        public TransactionModel NewTransaction
        {
            get => _newTransaction;
            set
            {
                _newTransaction = value;
                OnPropertyChanged(nameof(NewTransaction));
            }
        }

        public PagingViewModel Paging 
        { 
            get => _paging;
            set
            {
                _paging = value;
                OnPropertyChanged(nameof(Paging));
            }
        }

        public TransactionListViewModel TransactionListViewModel { get; }

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

            Title = "Home";
        }

        public async override Task Initialize()
        {
            NewTransaction = TransactionModel.GetWithDefaultValue();

            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.NumberOfLastTransaction);
            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            await SetTransactions();
            await SetCategories();
            await SetAccounts();
        }

        private void PagingModel_CurrentPageChanged(object sender, int e)
        {
            var pagingModel = (PagingModel)sender;

            Task.Run(async () => await SetTransactions(pagingModel));
        }

        private async Task SetAccounts()
        {
            Accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(await _accountService.GetAllAccounts()));
        }

        private async Task SetCategories()
        {
            Categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(await _categoryService.GetCategories(new List<Filter> { new Filter
                            {
                                Operation = Operations.Eq,
                                PropName = nameof(CategoryModel.IsSystem),
                                Value = false.ToString()
                            } })));
        }

        public async Task SetTransactions(PagingModel paging = null)
        {
            if(paging == null)
            {
                paging = new PagingModel
                {
                    CurrentPage = 1,
                    PageSize = _settings.NumberOfLastTransaction,
                    TotalItems = await _transactionService.CountTransactions()
                };
            }

            var pagingModel = _mapper.Map<Paging>(paging);

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetLastTransactions(pagingModel)));
        }

        public AsyncCommand AddTransactionCommand
        {
            get => _addTransaction != null ? _addTransaction 
                : _addTransaction = new AsyncCommand(async obj =>
                    {
                        var transactionEntity = _mapper.Map<TransactionDto>(NewTransaction);
                        await _transactionService.Add(transactionEntity);

                        await SetTransactions();

                        ResetCurrentTransaction();
                    });
        }

        public override string this[string columnName] => string.Empty;

        private void ResetCurrentTransaction()
        {
            NewTransaction.Quantity = null;
            NewTransaction.Description = null;
        }
    }
}
