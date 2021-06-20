namespace MoneyTrack.WPF.Client.Models
{
    public class AccountModel: BaseModel
    {
        private string _name;

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
    }
}
