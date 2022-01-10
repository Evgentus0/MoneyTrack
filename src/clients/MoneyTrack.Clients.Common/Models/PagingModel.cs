using System;

namespace MoneyTrack.Clients.Common.Models
{
    public class PagingModel : BaseModel
    {
        private int _totalItems;
        private int _currentPage;
        private int _pageSize;

        public event EventHandler<int> CurrentPageChanged;

        public int TotalItems 
        { 
            get => _totalItems;
            set
            {
                _totalItems = value;
                OnPropertyChanged(nameof(TotalItems));
            } 
        }
        public int CurrentPage 
        { 
            get => _currentPage;
            set
            {
                try
                {
                    if (value < 1 || value > CalcPageCount())
                    {
                        return;
                    }
                }
                catch(DivideByZeroException)
                {
                    // do nothing if divide by zero   
                }

                _currentPage = value;
                CurrentPageChanged?.Invoke(this, value);
                OnPropertyChanged(nameof(CurrentPage));
            } 
        }
        public int PageSize 
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged(nameof(PageSize));
            } 
        }

        public PagingModel()
        {
            CurrentPage = 1;
        }

        public int CalcPageCount()
        {
            if (PageSize <= 0)
                throw new DivideByZeroException();

            var result = TotalItems / PageSize;

            if (TotalItems % PageSize == 0)
            {
                return result;
            }

            return result + 1;
        }

        public override string this[string columnName] => string.Empty;
    }
}
