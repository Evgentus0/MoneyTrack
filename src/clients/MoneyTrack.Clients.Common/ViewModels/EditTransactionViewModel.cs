using MoneyTrack.Clients.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class EditTransactionViewModel : BaseViewModel
    {
        private TransactionModel _transactionModel;
        private ObservableCollection<AccountModel> _accounts;
        private ObservableCollection<CategoryModel> _categories;

        private string _errors;

        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public TransactionModel TransactionModel 
        {
            get => _transactionModel; 
            set
            {
                _transactionModel = value;
                OnPropertyChanged(nameof(TransactionModel));
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

        public string Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
