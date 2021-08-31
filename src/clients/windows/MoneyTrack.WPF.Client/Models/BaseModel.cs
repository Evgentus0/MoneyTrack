using System.ComponentModel;

namespace MoneyTrack.WPF.Client.Models
{
    public abstract class BaseModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public abstract string this[string columnName] { get; }

        public BaseModel()
        {
            Error = string.Empty;
        }

        public string Error { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
