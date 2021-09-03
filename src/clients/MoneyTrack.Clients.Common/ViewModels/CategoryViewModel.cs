using MoneyTrack.Clients.Common.Models;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
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
