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

            //var direction = column.SortDirection ??= ListSortDirection.Ascending;
            //column.SortDirection = direction;
            var sorting = new Sorting
            {
                PropName = column.SortMemberPath
            };

            
            viewModel.SortTransactions(sorting);
            //CollectionViewSource.

            //i do some custom checking based on column to get the right comparer
            //i have different comparers for different columns. I also handle the sort direction
            //in my comparer

            // prevent the built-in sort from sorting



            //set the sort order on the column


            //use a ListCollectionView to do the sort.


            //this is my custom sorter it just derives from IComparer and has a few properties
            //you could just apply the comparer but i needed to do a few extra bits and pieces


            //apply the sort
            //lcv.CustomSort = comparer;
        }
    }
}
