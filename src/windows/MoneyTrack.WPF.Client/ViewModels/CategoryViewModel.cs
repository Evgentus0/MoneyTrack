using MoneyTrack.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private CategoryModel _categoryModel;
        private string _errors;

        public CategoryModel CategoryModel
        {
            get => _categoryModel;
            set
            {
                _categoryModel = value;
                OnPropertyChanged(nameof(CategoryModel));
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

        public CategoryViewModel()
        {
            CategoryModel = new CategoryModel();
        }

        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
