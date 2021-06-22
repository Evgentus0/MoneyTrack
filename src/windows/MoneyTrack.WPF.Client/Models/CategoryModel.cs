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

        private Dictionary<string, Func<string>> _propertyValidation;
        private Dictionary<string, Func<string>> PropertyValidation => _propertyValidation ??= new Dictionary<string, Func<string>>
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
            })
        };
    }
}
