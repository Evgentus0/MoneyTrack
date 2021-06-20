namespace MoneyTrack.WPF.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private decimal _balance;

        public MainViewModel(HomeViewModel homeViewModel, AnalyticsViewModel analyticsViewModel)
        {
            HomeViewModel = homeViewModel;
            AnalyticsViewModel = analyticsViewModel;
        }

        public HomeViewModel HomeViewModel { get; set; }
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

        public override void Initialize()
        { }
    }
}
