using MaterialDesignThemes.Wpf;
using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Clients.Common.Models.Operational;
using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.WPF.Client.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoneyTrack.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for ManagingView.xaml
    /// </summary>
    public partial class ManagingView : BaseUserControl
    {
        public ManagingView()
        {
            InitializeComponent();
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ListView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private async void ListViewItem_PreviewMouseLeftButtonDown_Category(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                var selectedCategory = (CategoryModel)item.DataContext;
                if (selectedCategory is null)
                    return;

                var viewModel = (ManagingViewModel)DataContext;

                var previousCategory = new CategoryModel
                {
                    Name = selectedCategory.Name
                };

                var dialogViewModel = new CategoryViewModel();
                dialogViewModel.CategoryModel = selectedCategory;

                var view = new EditCategoryDialog
                {
                    DataContext = dialogViewModel
                };

                var result = await DialogHost.Show(view, "CategoryRootDialog", HandleCloseCategoryDialog);

                if (Enum.TryParse(result?.ToString(), out CloseDialogResult operation))
                {
                    switch (operation)
                    {
                        case CloseDialogResult.Update:
                            
                            viewModel.UpdateCategory(dialogViewModel.CategoryModel);
                            break;
                        case CloseDialogResult.Delete:
                            break;
                        case CloseDialogResult.Cancel:
                            var index = viewModel.Categories.IndexOf(selectedCategory);
                            viewModel.Categories.Remove(selectedCategory);
                            viewModel.Categories.Insert(index, previousCategory);
                            break;
                        case CloseDialogResult.Done:
                        default:
                            break;
                    }
                }
            }
        }

        private void HandleCloseCategoryDialog(object sender, DialogClosingEventArgs eventArgs)
        {

            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd == CloseDialogResult.Cancel ||
                    doAdd == CloseDialogResult.Delete)
                    return;
            }
            else return;


            var dialog = (UserControl)eventArgs.Session.Content;
            var dialogViewModel = (CategoryViewModel)dialog.DataContext;

            var validateResult = dialogViewModel.CategoryModel.ValidateModel();
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

        private async void Button_Click_Add_Category(object sender, RoutedEventArgs e)
        {
            var viewModel = (ManagingViewModel)DataContext;

            var dialogViewModel = new CategoryViewModel();

            var view = new AddCategoryDialog
            {
                DataContext = dialogViewModel
            };

            var result = await DialogHost.Show(view, "CategoryRootDialog", HandleCloseCategoryDialog);

            if (Enum.TryParse(result?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd == CloseDialogResult.Done)
                {
                    viewModel.AddCategory(dialogViewModel.CategoryModel);
                }
            }
        }

        private async void ListViewItem_PreviewMouseLeftButtonDown_Account(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                var selectedAccount = (AccountModel)item.DataContext;
                if (selectedAccount is null)
                    return;

                var viewModel = (ManagingViewModel)DataContext;

                var previousAccount = new AccountModel
                {
                    Id = selectedAccount.Id,
                    Name = selectedAccount.Name,
                    Balance = selectedAccount.Balance
                };

                var dialogViewModel = new AccountViewModel();
                dialogViewModel.AccountModel = selectedAccount;

                var view = new EditAccountDialog
                {
                    DataContext = dialogViewModel
                };

                var result = await DialogHost.Show(view, "AccountRootDialog", HandleCloseAccountDialog);

                if (Enum.TryParse(result?.ToString(), out CloseDialogResult operation))
                {
                    switch (operation)
                    {
                        case CloseDialogResult.Cancel:
                            var index = viewModel.Accounts.IndexOf(selectedAccount);
                            viewModel.Accounts.Remove(selectedAccount);
                            viewModel.Accounts.Insert(index, previousAccount);
                            break;
                        case CloseDialogResult.Update:
                            viewModel.UpdateAccount(dialogViewModel.AccountModel);
                            break;
                        case CloseDialogResult.Delete:
                            viewModel.DeleteAccount(dialogViewModel.AccountModel);
                            break;
                        case CloseDialogResult.Done:
                        default:
                            break;
                    }
                }
            }
        }

        private void HandleCloseAccountDialog(object sender, DialogClosingEventArgs eventArgs)
        {

            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd == CloseDialogResult.Cancel ||
                    doAdd == CloseDialogResult.Delete)
                    return;
            }
            else return;


            var dialog = (UserControl)eventArgs.Session.Content;
            var dialogViewModel = (AccountViewModel)dialog.DataContext;

            var validateResult = dialogViewModel.AccountModel.ValidateModel();
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

        private async void Button_Click_Add_Account(object sender, RoutedEventArgs e)
        {
            var viewModel = (ManagingViewModel)DataContext;

            var dialogViewModel = new AccountViewModel();

            var view = new AddAccountDialog
            {
                DataContext = dialogViewModel
            };

            var result = await DialogHost.Show(view, "AccountRootDialog", HandleCloseAccountDialog);

            if (Enum.TryParse(result?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd == CloseDialogResult.Done)
                {
                    viewModel.AddAccount(dialogViewModel.AccountModel);
                }
            }
        }
    }
}
