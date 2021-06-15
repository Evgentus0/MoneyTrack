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
    public class HomeViewModel : INotifyPropertyChanged
    {
        private TransactionModel _newTransaction;

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
        private bool _setCurrentDttm;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
