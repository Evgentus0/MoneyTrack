using AutoMapper;
using MaterialDesignThemes.Wpf;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Client.Commands;
using MoneyTrack.WPF.Client.Dialogs;
using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.Models.Operational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class ManagingViewModel : BaseViewModel
    {
        private ObservableCollection<CategoryModel> _categories;
        private ObservableCollection<AccountModel> _accounts;
        private CategoryModel _selectedCategory;
        private AccountModel _selectedAccount;
        private AsyncCommand _addCategoryCommand;
        private AsyncCommand _addAccountCommand;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ObservableCollection<CategoryModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ObservableCollection<AccountModel> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }

        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                SelectCategory();
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private async Task SelectCategory()
        {
            if (SelectedCategory is null)
                return;

            var dialogViewModel = new CategoryViewModel();
            dialogViewModel.CategoryModel = SelectedCategory;

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
                        var categoryDto = _mapper.Map<CategoryDto>(dialogViewModel.CategoryModel);
                        await _categoryService.Update(categoryDto);
                        break;
                    case CloseDialogResult.Delete:
                        await _categoryService.Delete(dialogViewModel.CategoryModel.Id);
                        Categories.Remove(dialogViewModel.CategoryModel);
                        break;
                    case CloseDialogResult.Done:
                    case CloseDialogResult.Cancel:
                    default:
                        break;
                }
            }

            SelectedCategory = null;
        }

        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                SelectAccount();
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }

        private async Task SelectAccount()
        {
            if (SelectedAccount is null)
                return;

            var dialogViewModel = new AccountViewModel();
            dialogViewModel.AccountModel = SelectedAccount;

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
                        break;
                    case CloseDialogResult.Update:
                        var accountDto = _mapper.Map<AccountDto>(dialogViewModel.AccountModel);
                        await _accountService.Update(accountDto);
                        break;
                    case CloseDialogResult.Delete:
                        await _accountService.Delete(dialogViewModel.AccountModel.Id);
                        Accounts.Remove(dialogViewModel.AccountModel);
                        break;
                    case CloseDialogResult.Done:
                    default:
                        break;
                }
            }

            SelectedAccount = null;
        }

        public AsyncCommand AddCategoryCommand 
        { 
            get => _addCategoryCommand ??= new AsyncCommand(async obj =>
            {
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
                        Categories.Add(dialogViewModel.CategoryModel);
                        var categoryDto = _mapper.Map<CategoryDto>(dialogViewModel.CategoryModel);
                        await _categoryService.AddCategory(categoryDto);
                    }
                }
            });
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

        public AsyncCommand AddAccountCommand
        {
            get => _addAccountCommand ??= new AsyncCommand(async obj =>
            {
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
                        Accounts.Add(dialogViewModel.AccountModel);
                        var accountDto = _mapper.Map<AccountDto>(dialogViewModel.AccountModel);
                        await _accountService.AddAccount(accountDto);
                    }
                }
            });
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

        public ManagingViewModel(
            ICategoryService categoryService,
            IAccountService accountService,
            IMapper mapper
            )
        {
            _categoryService = categoryService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public override string this[string columnName] => string.Empty;

        public async override Task Initialize()
        {
            await SetCategories();
            await SetAccounts();
        }

        private async Task SetAccounts()
        {
            Accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(await _accountService.GetAllAccounts()));
        }

        private async Task SetCategories()
        {
            Categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(await _categoryService.GetAllCategories()));
        }
    }
}
