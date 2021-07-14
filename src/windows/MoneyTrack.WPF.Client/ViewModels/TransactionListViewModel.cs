using AutoMapper;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.Models;
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
        private readonly AppSettings _settings;

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

        public List<string> PropertiesList { get; private set; }

        public TransactionListViewModel(ITransactionService transactionService,
            IMapper mapper,
            AppSettings settings)
        {
            _transactionService = transactionService;
            _mapper = mapper;
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

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetQueryTransactions(_dbRequest)));
        }

        public override async Task Initialize()
        {
            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.TransactionPageSize);
            _dbRequest.Paging = _mapper.Map<Paging>(Paging.PagingModel);

            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            InitPropLists();

            Filters = new ObservableCollection<FilterModel>()
            {
                new FilterModel
                {
                    PropName = "prop1",
                    Operation = Operations.Eq,
                    Value = "val1"
                },
                new FilterModel
                {
                    PropName="prop2",
                    Operation = Operations.Less,
                    Value = "val2"
                }
            };

            await SetTransactions();
        }

        private Dictionary<string, bool> _propSortingDirect;
        private ObservableCollection<FilterModel> _filters;

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
