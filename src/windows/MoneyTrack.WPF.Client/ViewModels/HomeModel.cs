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
        public decimal Number { get; set; }
        public string Description { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
