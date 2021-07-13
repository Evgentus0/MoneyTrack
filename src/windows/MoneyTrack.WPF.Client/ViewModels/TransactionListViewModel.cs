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
            if (_dbRequest.Paging== null)
            {
                _dbRequest.Paging = new Paging
                {
                    CurrentPage = 1,
                    PageSize = _settings.NumberOfLastTransaction,
                    TotalItems = await _transactionService.CountTransactions()
                };
            }

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetQueryTransactions(_dbRequest)));
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
