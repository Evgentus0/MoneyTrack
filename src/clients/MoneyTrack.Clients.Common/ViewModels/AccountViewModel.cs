using MoneyTrack.Clients.Common.Models;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class AccountViewModel: BaseViewModel
    {
        private AccountModel _accountModel;
        private string _errors;

        public AccountModel AccountModel
        {
            get => _accountModel;
            set
            {
                _accountModel = value;
                OnPropertyChanged(nameof(AccountModel));
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

        public AccountViewModel()
        {
            AccountModel = new AccountModel();
        }

        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
