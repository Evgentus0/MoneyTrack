using MoneyTrack.Core.Models.Operational;
using System;

namespace MoneyTrack.Clients.Common.Models
{
    public class FilterModel : BaseModel
    {
        private string _propName;
        private string _value;
        private Operations _operation;
        private FilterOp _filterOp;

        public override string this[string columnName] => string.Empty;

        public string PropName
        {
            get => _propName;
            set
            {
                _propName = value;
                OnPropertyChanged(nameof(PropName));

                PropertyUpdated.Invoke(this, value);
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

        public FilterOp FilterOp 
        {
            get => _filterOp;
            set
            {
                _filterOp = value;
                OnPropertyChanged(nameof(FilterOp));
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

        public event EventHandler<string> PropertyUpdated;
    }
}
