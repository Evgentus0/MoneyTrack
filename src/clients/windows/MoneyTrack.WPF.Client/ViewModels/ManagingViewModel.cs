using AutoMapper;
using MaterialDesignThemes.Wpf;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
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

        public void DeleteCategory(CategoryModel category)
        {
            Task.Run(async () =>
            {
                await _categoryService.Delete(category.Id);
                Categories.Remove(category);
            });
        }
        public void UpdateCategory(CategoryModel category)
        {
            Task.Run(async () =>
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                await _categoryService.Update(categoryDto);
            });
        }
        public void AddCategory(CategoryModel category)
        {
            Task.Run(async () =>
            {
                Categories.Add(category);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                await _categoryService.AddCategory(categoryDto);
            });
        }

        public void UpdateAccount(AccountModel account)
        {
            Task.Run(async () =>
            {
                var accountDto = _mapper.Map<AccountDto>(account);
                await _accountService.Update(accountDto, true);
            });
        }

        public void DeleteAccount(AccountModel account)
        {
            Task.Run(async () =>
            {
                await _accountService.Delete(account.Id);
                Accounts.Remove(account);
            });
        }

        public void AddAccount(AccountModel account)
        {
            Task.Run(async () =>
            {
                Accounts.Add(account);
                var accountDto = _mapper.Map<AccountDto>(account);
                await _accountService.AddAccount(accountDto);
            });
        }

        private async Task SetAccounts()
        {
            Accounts = new ObservableCollection<AccountModel>
                            (_mapper.Map<List<AccountModel>>(await _accountService.GetAllAccounts()));
        }

        private async Task SetCategories()
        {
            Categories = new ObservableCollection<CategoryModel>
                            (_mapper.Map<List<CategoryModel>>(await _categoryService.GetCategories(new List<Filter> { new Filter 
                            {
                                Operation = Operations.Eq,
                                PropName = nameof(CategoryModel.IsSystem),
                                Value = false.ToString()
                            } })));
        }
    }
}
