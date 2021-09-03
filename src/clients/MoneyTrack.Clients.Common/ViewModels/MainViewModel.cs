using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private decimal _balance;

        public MainViewModel(
            HomeViewModel homeViewModel,
            ManagingViewModel managingViewModel,
            AnalyticsViewModel analyticsViewModel)
        {
            HomeViewModel = homeViewModel;
            ManagingViewModel = managingViewModel;
            AnalyticsViewModel = analyticsViewModel;
        }

        public override string this[string columnName] => string.Empty;

        public HomeViewModel HomeViewModel { get; set; }
        public ManagingViewModel ManagingViewModel { get; }
        public AnalyticsViewModel AnalyticsViewModel { get; }

        public decimal Balance 
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            } 
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
