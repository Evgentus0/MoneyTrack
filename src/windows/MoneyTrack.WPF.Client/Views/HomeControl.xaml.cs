using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyTrack.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            Data = new HomeModel();

            Data.LastTransactions.AddRange(new List<TransactionModel>() 
            {
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                 new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},
                new TransactionModel{Account="Account1", AddedDttm=DateTime.FromOADate(32123), Category="Category1", Description="Description1", Quantity=1},
                new TransactionModel{Account="Account2", AddedDttm=DateTime.FromOADate(1238), Category="Category2", Description="Description2", Quantity=2},
                new TransactionModel{Account="Account4", AddedDttm=DateTime.FromOADate(96741), Category="Category4", Description="Description4", Quantity=4},

            });
            
            DataContext = Data;

        }
        public HomeModel Data { get; set; }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
