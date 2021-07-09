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

        public TransactionListViewModel(ITransactionService transactionService, 
            IMapper mapper,
            AppSettings settings)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _settings = settings;
        }

        public override string this[string columnName] => string.Empty;

        private async Task SetTransactions(
            PagingModel paging = null, 
            Sorting sorting = null,
            List<Filter> filters = null)
        {
            if (paging == null)
            {
                paging = new PagingModel
                {
                    CurrentPage = 1,
                    PageSize = _settings.NumberOfLastTransaction,
                    TotalItems = await _transactionService.CountTransactions()
                };
            }

            var pagingModel = _mapper.Map<Paging>(paging);

            var request = new DbQueryRequest
            {
                Paging = pagingModel,
                Sorting = sorting,
                Filters = filters
            };

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetQueryTransactions(request)));
        }

        public override async Task Initialize()
        {
            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.NumberOfLastTransaction);
            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            InitPropSortingDirect();

            await SetTransactions();
        }

        private Dictionary<string, bool> _propSortingDirect;
        private void InitPropSortingDirect()
        {
            var properties = typeof(TransactionModel).GetProperties();
            _propSortingDirect = new Dictionary<string, bool>();
            foreach(var propInfo in properties)
            {
                if(propInfo.PropertyType.IsClass)
                {
                    var objProps = propInfo.PropertyType.GetProperties();
                    foreach (var objPropInfo in objProps)
                    {
                        _propSortingDirect.Add($"{propInfo.Name}.{objPropInfo.Name}", true);
                    }
                }
                _propSortingDirect.Add(propInfo.Name, true);
            }
        }

        private void PagingModel_CurrentPageChanged(object sender, int e)
        {
            var pagingModel = (PagingModel)sender;

            Task.Run(async () => await SetTransactions(pagingModel));
        }

        public void SortTransactions(Sorting sorting)
        {
            sorting.Direction = _propSortingDirect[sorting.PropName] ? SortDirect.Asc : SortDirect.Desc;
            _propSortingDirect[sorting.PropName] = !_propSortingDirect[sorting.PropName];

            Task.Run(async () => await SetTransactions(sorting: sorting));
        }
    }
}
