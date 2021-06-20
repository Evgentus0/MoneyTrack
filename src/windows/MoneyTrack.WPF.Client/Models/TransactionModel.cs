using System;

namespace MoneyTrack.WPF.Client.Models
{
    public class TransactionModel : BaseModel
    {
        #region Fields
        private decimal? _quantity;
        private string _description;
        private CategoryModel _category;
        private AccountModel _account;
        private DateTimeOffset? _addedDttm;
        private bool _setCurrentDttm;
        #endregion

        #region Properties
        public int Id { get; set; }
        public decimal? Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public CategoryModel Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public AccountModel Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(nameof(Account));
            }
        }
        public DateTimeOffset? AddedDttm
        {
            get => _addedDttm;
            set
            {
                _addedDttm = value;
                OnPropertyChanged(nameof(AddedDttm));
            }
        }
        public bool SetCurrentDttm
        {
            get => _setCurrentDttm;
            set
            {
                _setCurrentDttm = value;
                OnPropertyChanged(nameof(SetCurrentDttm));
            }
        }
        #endregion

        public TransactionModel()
        {
            Category = new CategoryModel();
            Account = new AccountModel();
            SetCurrentDttm = true;
        }

        
    }
}
