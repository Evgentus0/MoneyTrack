using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.Models
{
    public class FilterModel : BaseModel
    {
        private string _propName;
        private string _value;
        private Operations _operation;

        public override string this[string columnName] => string.Empty;

        public string PropName
        {
            get => _propName;
            set
            {
                _propName = value;
                OnPropertyChanged(nameof(PropName));
            }
        }
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public Operations Operation
        { 
            get => _operation; 
            set
            {
                _operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }

        public string ValidateModel()
        {
            var result = string.Empty;

            if (string.IsNullOrEmpty(PropName))
            {
                result += "Property should not be empty \n";
            }

            if (string.IsNullOrEmpty(Value))
            {
                result += "Value should not be empty \n";
            }

            return result;
        }
    }
}
