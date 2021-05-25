using MoneyTrack.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class HomeModel
    {
        public List<string> Categories { get; set; }
        public List<string> Accounts { get; set; }
        public List<TransactionModel> LastTransactions { get; set; }

        public HomeBidirectModel BidirectModel { get; set; }

        public HomeModel()
        {
            Categories = new List<string>();
            Accounts = new List<string>();
            BidirectModel = new HomeBidirectModel();
            LastTransactions = new List<TransactionModel>();
        }
    }

    public class HomeBidirectModel : INotifyPropertyChanged
    {
        private decimal _number;
        public decimal Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
