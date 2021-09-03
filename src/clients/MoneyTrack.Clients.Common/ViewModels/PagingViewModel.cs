using MoneyTrack.Clients.Common.Commands;
using MoneyTrack.Clients.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class PagingViewModel : BaseViewModel
    {
        private List<int> _pages;
        private PagingModel _pagingModel;
        private object _items;

        private RelayCommand _previousPage;
        private RelayCommand _nextPage;

        public PagingModel PagingModel
        {
            get => _pagingModel;
            set
            {
                _pagingModel = value;
                OnPropertyChanged(nameof(PagingModel));
            }
        }

        public List<int> Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        public object Items 
        { 
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            } 
        }

        private void SetPages()
        {
            Pages = Enumerable.Range(1, PagingModel.CalcPageCount()).ToList();
        }


        public PagingViewModel(int totalCount, int pageSize)
        {
            PagingModel = new PagingModel
            {
                TotalItems = totalCount,
                PageSize = pageSize
            };

            SetPages();
        }

        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public RelayCommand PreviousPage
        {
            get
            {
                return _previousPage ??= new RelayCommand(obj =>
                {
                    PagingModel.CurrentPage--;
                });
            }
        }

        public RelayCommand NextPage
        {
            get
            {
                return _nextPage ??= new RelayCommand(obj =>
                {
                    PagingModel.CurrentPage++;
                });
            }
        }
    }
}
