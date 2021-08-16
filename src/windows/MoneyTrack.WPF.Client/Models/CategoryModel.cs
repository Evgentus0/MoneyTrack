using System;
using System.Collections.Generic;

namespace MoneyTrack.WPF.Client.Models
{
    public class CategoryModel : BaseModel
    {
        private string _name;

        public override string this[string columnName] => PropertyValidation[columnName]();

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public bool IsSystem 
        {
            get => _isSystem;
            set
            {
                _isSystem = value;
                OnPropertyChanged(nameof(IsSystem));
            }
        }

        private Dictionary<string, Func<string>> _propertyValidation;
        private bool _isSystem;

        private Dictionary<string, Func<string>> PropertyValidation => _propertyValidation ??= new Dictionary<string, Func<string>>
        {
            [nameof(Id)] = new Func<string>(() =>
            {
                var result = string.Empty;
                var message = "Incorrect Id Value";

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
