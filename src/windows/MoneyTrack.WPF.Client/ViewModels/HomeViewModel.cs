using MoneyTrack.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private TransactionModel _newTransaction;
        private bool _setCurrentDttm;

        public List<string> Categories { get; set; }
        public List<string> Accounts { get; set; }
        public ObservableCollection<TransactionModel> LastTransactions { get; set; }
        public TransactionModel NewTransaction
        {
            get { return _newTransaction; }
            set
            {
                _newTransaction = value;
                OnPropertyChanged(nameof(NewTransaction));
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

        public HomeViewModel()
        {
            Categories = new List<string>();
            Accounts = new List<string>();
            LastTransactions = new ObservableCollection<TransactionModel>();
        }

        public override void Initialize()
        {
            Accounts.Add("acc1");
            Accounts.Add("acc2");
        }
    }    
}
