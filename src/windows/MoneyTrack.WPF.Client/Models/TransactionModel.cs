using System;
using System.Collections.Generic;

namespace MoneyTrack.WPF.Client.Models
{
    public class TransactionModel : BaseModel
    {
        #region Fields
        private decimal? _quantity;
        private string _description;
        private CategoryModel _category;
        private AccountModel _account;
        private DateTimeOffset? _addedDttm;
        private bool _setCurrentDttm;
        #endregion

        #region Properties
        public int Id { get; set; }
        public decimal? Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public CategoryModel Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public AccountModel Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(nameof(Account));
            }
        }
        public DateTimeOffset? AddedDttm
        {
            get => _addedDttm;
            set
            {
                _addedDttm = value;
                OnPropertyChanged(nameof(AddedDttm));
            }
        }
        public bool SetCurrentDttm
        {
            get => _setCurrentDttm;
            set
            {
                _setCurrentDttm = value;
                OnPropertyChanged(nameof(SetCurrentDttm));
            }
        }

        #endregion

        protected TransactionModel()
        {
            Category = new CategoryModel();
            Account = new AccountModel();
        }

        public static TransactionModel GetWithDefaultValue()
        {
            return new TransactionModel()
            {
                SetCurrentDttm = true,
            };
        }

        public override string this[string columnName] => PropertyValidation[columnName]();

        private Dictionary<string, Func<string>> _propertyValidation;
        private Dictionary<string, Func<string>> PropertyValidation => _propertyValidation ??= new Dictionary<string, Func<string>>
        {
            [nameof(Quantity)] = new Func<string>(() => 
            {
                var result = string.Empty;
                var message = $"{nameof(Quantity)} can not be 0; ";
                if (Quantity == 0)
                {
                    result = message;
                    Error += message;
                }
                else
                {
                    Error.Replace(message, string.Empty);
                }

                return result;
            }),
            [nameof(Description)] = new Func<string>(() =>
            {
                if(Description != null && string.IsNullOrWhiteSpace(Description))
                {
                    return "Description can not be emty";
                }
                return string.Empty;
            }),
            [nameof(AddedDttm)] = new Func<string>(() =>
            {
                var result = string.Empty;

                if(!SetCurrentDttm && AddedDttm is null)
                {
                    result = $"Date can not be empty";
                }

                return result;
            })
        };
    }
}
