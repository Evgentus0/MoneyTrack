using System;
using System.Collections.Generic;

namespace MoneyTrack.Clients.Common.Models
{
    public class AccountModel : BaseModel
    {
        private string _name;
        private int _id;
        private decimal? _balance;

        public override string this[string columnName] => PropertyValidation[columnName]();

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public decimal? Balance 
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            } 
        }

        private Dictionary<string, Func<string>> _propertyValidation;

        private Dictionary<string, Func<string>> PropertyValidation => _propertyValidation != null ? _propertyValidation 
            : _propertyValidation = new Dictionary<string, Func<string>>
        {
            [nameof(Id)] = new Func<string>(() =>
            {
                var result = string.Empty;
                var message = "Incorrect Value";

                if (Id < 0)
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
            [nameof(Name)] = new Func<string>(() =>
            {
                var result = string.Empty;
                var message = "Incorrect Name Value";

                if (string.IsNullOrEmpty(Name))
                {
                    result = message;
                    Error += message;
                }
                else
                {
                    Error.Replace(message, string.Empty);
                }

                return result;
            })
        };

        public string ValidateModel()
        {
            return PropertyValidation[nameof(Name)]();
        }
    }
}
