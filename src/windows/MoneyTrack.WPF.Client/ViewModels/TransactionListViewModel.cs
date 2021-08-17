using AutoMapper;
using MaterialDesignThemes.Wpf;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.Commands;
using MoneyTrack.WPF.Client.Dialogs;
using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.Models.Operational;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class TransactionListViewModel : BaseViewModel
    {
        private PagingViewModel _paging;

        private DbQueryRequest _dbRequest;

        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly AppSettings _settings;

        private ObservableCollection<AccountModel> _accounts;
        private ObservableCollection<CategoryModel> _categories;

        public PagingViewModel Paging
        {
            get => _paging;
            set
            {
                _paging = value;
                OnPropertyChanged(nameof(Paging));
            }
        }

        public ObservableCollection<FilterModel> Filters
        {
            get => _filters;
            set
            {
                _filters = value;
                OnPropertyChanged(nameof(Filters));
            }
        }

        public FilterModel FilterToDelete
        {
            get => _filterToDelete;
            set
            {
                _filterToDelete = value;
                Filters.Remove(value);
                OnPropertyChanged(nameof(FilterToDelete));
            }
        }

        public decimal TotalBalance
        {
            get => _totalBalance;
            set
            {
                _totalBalance = value;
                OnPropertyChanged(nameof(TotalBalance));
            }
        }

        public List<string> PropertiesList { get; private set; }

        public AsyncCommand AddFilterDialogCommand
        {
            get => _addFilterDialogCommand ??= new AsyncCommand(async obj =>
            {
                var dialogViewModel = new FilterViewModel(PropertiesList, Enum.GetNames(typeof(Operations)).ToList());

                var view = new AddNewFilterDialog
                {
                    DataContext = dialogViewModel
                };

                var result = await DialogHost.Show(view, "RootDialog", HandleFilterCloseDialog);

                if (bool.TryParse(result?.ToString(), out bool doAdd))
                {
                    if (doAdd)
                    {
                        Filters.Add(dialogViewModel.FilterModel);
                    }
                }
            });
        }

        public AsyncCommand ApplyFiltersCommand 
        {
            get => _applyFiltersCommand ??= new AsyncCommand(async obj =>
            {
                await SetTransactions();
            });
        }

        private void HandleFilterCloseDialog(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool isAccept)
            {
                if (!isAccept)
                    return;
            }
            else
                return;

            var dialog = (AddNewFilterDialog)eventArgs.Session.Content;
            var dialogViewModel = (FilterViewModel)dialog.DataContext;

            var validateResult = dialogViewModel.FilterModel.ValidateModel();
            if (string.IsNullOrEmpty(validateResult))
            {
                return;
            }
            else
            {
                dialogViewModel.Errors = validateResult;
                eventArgs.Cancel();
            }
        }

        public TransactionListViewModel(ITransactionService transactionService,
            IMapper mapper,
            ICategoryService categoryService,
            IAccountService accountService,
            AppSettings settings)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _categoryService = categoryService;
            _accountService = accountService;
            _settings = settings;

            _dbRequest = new DbQueryRequest();
        }

        public override string this[string columnName] => string.Empty;

        private async Task SetTransactions()
        {
            if (_dbRequest.Paging == null)
            {
                _dbRequest.Paging = new Paging
                {
                    CurrentPage = 1,
                    PageSize = _settings.TransactionPageSize,
                    TotalItems = await _transactionService.CountTransactions()
                };
            }

            _dbRequest.Filters = _mapper.Map<List<Filter>>(Filters.ToList());

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetQueryTransactions(_dbRequest)));

            TotalBalance = await _transactionService.CalculateTotalBalance(_dbRequest.Filters);
        }

        private async Task SetAccounts()
        {
            _accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(await _accountService.GetAllAccounts()));
        }

        private async Task SetCategories()
        {
            _categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(await _categoryService.GetCategories(new List<Filter> { new Filter
                            {
                                Operation = Operations.Eq,
                                PropName = nameof(CategoryModel.IsSystem),
                                Value = false.ToString()
                            } })));
        }

        public override async Task Initialize()
        {
            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.TransactionPageSize);
            _dbRequest.Paging = _mapper.Map<Paging>(Paging.PagingModel);

            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            InitPropLists();

            Filters = new ObservableCollection<FilterModel>();

            TransactionModel.TransactionDeleted += TransactionModel_TransactionDeleted;
            TransactionModel.TransactionUpdated += TransactionModel_TransactionUpdated;

            await SetTransactions();
            await SetCategories();
            await SetAccounts();
        }

        private void TransactionModel_TransactionUpdated(object sender, EventArgs e)
        {
            var transaction = (TransactionModel)sender;

            OpenEditTransactionDialog(transaction);
        }

        private async Task OpenEditTransactionDialog(TransactionModel transaction)
        {
            var dialogVIewModel = new EditTransactionViewModel()
            {
                TransactionModel = transaction,
                Categories = _categories,
                Accounts = _accounts
            };

            var view = new EditTransactionDialog
            {
                DataContext = dialogVIewModel
            };

            var result = await DialogHost.Show(view, "RootDialog", HandleEditTransactionDialogClose);

            if (Enum.TryParse(result?.ToString(), out CloseDialogResult action))
            {
                if (action == CloseDialogResult.Update)
                {
                    await _transactionService.Update(_mapper.Map<TransactionDto>(dialogVIewModel.TransactionModel));
                }
            }
        }

        private void HandleEditTransactionDialogClose(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult action))
            {
                if (action == CloseDialogResult.Cancel ||
                    action == CloseDialogResult.Delete)
                    return;
            }
            else return;


            var dialog = (UserControl)eventArgs.Session.Content;
            var dialogViewModel = (EditTransactionViewModel)dialog.DataContext;

            string validateResult = dialogViewModel.TransactionModel.ValidateModel();
            if (string.IsNullOrEmpty(validateResult))
            {
                return;
            }
            else
            {
                dialogViewModel.Errors = validateResult;
                eventArgs.Cancel();
            }
        }

        private void TransactionModel_TransactionDeleted(object sender, int e)
        {
            try
            {
                var items = (ObservableCollection<TransactionModel>)Paging.Items;

                items.Remove(items.First(x => x.Id == e));

                Task.Run(async () => await _transactionService.Delete(e));
            }
            catch { }
        }

        private Dictionary<string, bool> _propSortingDirect;
        private ObservableCollection<FilterModel> _filters;
        private AsyncCommand _addFilterDialogCommand;
        private FilterModel _filterToDelete;
        private decimal _totalBalance;
        private AsyncCommand _applyFiltersCommand;

        private void InitPropLists()
        {
            PropertiesList = new List<string>
            {
                nameof(TransactionModel.Quantity),
                nameof(TransactionModel.Description),
                nameof(TransactionModel.Category) + "." + nameof(TransactionModel.Category.Name),
                nameof(TransactionModel.Account) + "." + nameof(TransactionModel.Account.Name),
                nameof(TransactionModel.AddedDttm),
            };

            _propSortingDirect = new Dictionary<string, bool>();
            foreach (var item in PropertiesList)
            {
                _propSortingDirect.Add(item, true);
            }
        }

        private void PagingModel_CurrentPageChanged(object sender, int e)
        {
            var pagingModel = (PagingModel)sender;
            _dbRequest.Paging = _mapper.Map<Paging>(pagingModel);
            Task.Run(async () => await SetTransactions());
        }

        public void SortTransactions(Sorting sorting)
        {
            sorting.Direction = _propSortingDirect[sorting.PropName] ? SortDirect.Asc : SortDirect.Desc;
            _propSortingDirect[sorting.PropName] = !_propSortingDirect[sorting.PropName];

            _dbRequest.Sorting = sorting;

            Task.Run(async () => await SetTransactions());
        }
    }
}
