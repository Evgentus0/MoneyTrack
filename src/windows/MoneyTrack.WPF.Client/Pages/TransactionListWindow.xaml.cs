using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyTrack.WPF.Client.Pages
{
    /// <summary>
    /// Interaction logic for TransactionListWindow.xaml
    /// </summary>
    public partial class TransactionListWindow : BaseWindow
    {
        public TransactionListWindow()
        {
            InitializeComponent();

            
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var viewModel = (TransactionListViewModel)DataContext;
            DataGridColumn column = e.Column;
            e.Handled = true;

            var sorting = new Sorting
            {
                PropName = column.SortMemberPath
            };

            
            viewModel.SortTransactions(sorting);
        }
    }
}
