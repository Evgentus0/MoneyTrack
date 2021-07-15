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
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
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

                var result = await DialogHost.Show(view, "CategoryRootDialog", HandleCloseCategoryAddDialog);

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

        private void HandleCloseCategoryAddDialog(object sender, DialogClosingEventArgs eventArgs)
        {

            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd != CloseDialogResult.Done)
                    return;
            }
            else return;
                   

            var dialog = (AddCategoryDialog)eventArgs.Session.Content;
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

                var result = await DialogHost.Show(view, "AccountRootDialog", HandleCloseAccountAddDialog);

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

        private void HandleCloseAccountAddDialog(object sender, DialogClosingEventArgs eventArgs)
        {

            if (Enum.TryParse(eventArgs.Parameter?.ToString(), out CloseDialogResult doAdd))
            {
                if (doAdd != CloseDialogResult.Done)
                    return;
            }
            else return;


            var dialog = (AddAccountDialog)eventArgs.Session.Content;
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
