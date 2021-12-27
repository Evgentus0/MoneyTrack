using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Core.DomainServices.Attributes;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private FilterModel _filterModel;
        private List<string> _properties;
        private List<string> _operations;
        private string _errors;

        private Dictionary<string, Type> _propertyTypes;

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

        public FilterViewModel(Dictionary<string, Type> properties, FilterOp filterOp)
        {
            _propertyTypes = properties;

            Properties = properties.Keys.ToList();
            FilterModel = new FilterModel
            {
                FilterOp = filterOp
            };

            FilterModel.PropertyUpdated += FilterModel_PropertyUpdated;
        }

        private void FilterModel_PropertyUpdated(object sender, string e)
        {
            Operations = Filter.GetAvailableOperations(_propertyTypes[e]).Select(x => x.ToString()).ToList();
        }

        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
