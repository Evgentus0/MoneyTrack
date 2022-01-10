using MaterialDesignThemes.Wpf;
using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Clients.Common.Models.Operational;
using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private async void Button_Click_AddFilter(object sender, RoutedEventArgs e)
        {
            var viewModel = (TransactionListViewModel)DataContext;

            var button = (Button)sender;
            var filterOp = button.Tag != null ? (FilterOp)Enum.Parse(typeof(FilterOp), (string)button.Tag)
                : FilterOp.And;

            var dialogViewModel = new FilterViewModel(viewModel.PropertiesList, filterOp);

            var view = new AddNewFilterDialog
            {
                DataContext = dialogViewModel
            };

            var result = await DialogHost.Show(view, "RootDialog", HandleFilterCloseDialog);

            if (bool.TryParse(result?.ToString(), out bool doAdd))
            {
                if (doAdd)
                {
                    viewModel.Filters.Add(dialogViewModel.FilterModel);
                    viewModel.IsFirstFilter = false;
                }
            }
        }

        private void HandleFilterCloseDialog(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool isAccept)
            {
                if (!isAccept)
                    return;
            }
            else
                return;

            var dialog = (AddNewFilterDialog)eventArgs.Session.Content;
            var dialogViewModel = (FilterViewModel)dialog.DataContext;

            var validateResult = dialogViewModel.FilterModel.ValidateModel();
            if (string.IsNullOrEmpty(validateResult))
            {
                return;
            }
            else
            {
                dialogViewModel.Errors = validateResult;
                eventArgs.Cancel();
            }
        }

        private async void MenuItem_Click_Edit_Transaction(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            var transaction = (TransactionModel)menuItem.DataContext;

            var viewModel = (TransactionListViewModel)DataContext;

            var dialogVIewModel = new EditTransactionViewModel()
            {
                TransactionModel = transaction,
                Categories = viewModel.Categories,
                Accounts = viewModel.Accounts
            };

            var view = new EditTransactionDialog
            {
                DataContext = dialogVIewModel
            };

            var result = await DialogHost.Show(view, "RootDialog", HandleEditTransactionDialogClose);

            if (Enum.TryParse(result?.ToString(), out CloseDialogResult action))
            {
                if (action == CloseDialogResult.Update)
                {
                    viewModel.UpdateTransaction(dialogVIewModel.TransactionModel);
                }
            }
        }

        private void HandleEditTransactionDialogClose(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult action))
            {
                if (action == CloseDialogResult.Cancel ||
                    action == CloseDialogResult.Delete)
                    return;
            }
            else return;


            var dialog = (UserControl)eventArgs.Session.Content;
            var dialogViewModel = (EditTransactionViewModel)dialog.DataContext;

            string validateResult = dialogViewModel.TransactionModel.ValidateModel();
            if (string.IsNullOrEmpty(validateResult))
            {
                return;
            }
            else
            {
                dialogViewModel.Errors = validateResult;
                eventArgs.Cancel();
            }
        }
    }
}
