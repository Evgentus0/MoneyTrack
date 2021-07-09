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

        private async Task SetLastTransactions(PagingModel paging = null)
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

            Paging.Items = new ObservableCollection<TransactionModel>
                (_mapper.Map<List<TransactionModel>>(await _transactionService.GetLastTransactions(pagingModel)));
        }

        public override async Task Initialize()
        {
            Paging = new PagingViewModel(await _transactionService.CountTransactions(), _settings.NumberOfLastTransaction);
            Paging.PagingModel.CurrentPageChanged += PagingModel_CurrentPageChanged;

            await SetLastTransactions();
        }

        private void PagingModel_CurrentPageChanged(object sender, int e)
        {
            var pagingModel = (PagingModel)sender;

            Task.Run(async () => await SetLastTransactions(pagingModel));
        }
    }
}
