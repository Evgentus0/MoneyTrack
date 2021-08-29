using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private FilterModel _filterModel;
        private List<string> _properties;
        private List<string> _operations;
        private string _errors;

        public FilterModel FilterModel
        {
            get => _filterModel;
            set
            {
                _filterModel = value;
                OnPropertyChanged(nameof(FilterModel));
            }
        }

        public List<string> Properties
        {
            get => _properties;
            set
            {
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }

        public List<string> Operations
        {
            get => _operations;
            set
            {
                _operations = value;
                OnPropertyChanged(nameof(Operations));
            }
        }

        public string Errors 
        {
            get => _errors;
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        public FilterViewModel(List<string> properties, List<string> operations)
        {
            Properties = properties;
            Operations = operations;
            FilterModel = new FilterModel();
        }


        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
