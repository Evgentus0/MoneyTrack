using AutoMapper;
using MoneyTrack.Clients.Common.Commands;
using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Clients.Common.Settings;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
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

        public AsyncCommand ApplyFiltersCommand 
        {
            get => _applyFiltersCommand ??= new AsyncCommand(async obj =>
            {
                await SetTransactions();
            });
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

        public void UpdateTransaction(TransactionModel transactionModel)
        {
            Task.Run(async () =>
            {
                await _transactionService.Update(_mapper.Map<TransactionDto>(transactionModel));
            });
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

        public override async Task Initialize()
        {
            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.TransactionPageSize);
            _dbRequest.Paging = _mapper.Map<Paging>(Paging.PagingModel);

            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            InitPropLists();

            Filters = new ObservableCollection<FilterModel>();

            TransactionModel.TransactionDeleted += TransactionModel_TransactionDeleted;
            TransactionModel.PostponedTransactionApproved += TransactionModel_PostponedTransactionApproved;

            await SetTransactions();
            await SetCategories();
            await SetAccounts();
        }

        private void TransactionModel_PostponedTransactionApproved(object sender, int e)
        {
            Task.Run(async () => await _transactionService.ApprovePostponedTransaction(e));
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
