using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.WPF.Client.Views;
using System.Windows;

namespace MoneyTrack.WPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void InitializeResources()
        {
            var context = (MainViewModel)DataContext;

            var homeTemplate = new FrameworkElementFactory(typeof(HomeControl));
            homeTemplate.SetValue(DataContextProperty, context.HomeViewModel);
            Resources["HomeViewTemplate"] = new DataTemplate
            {
                VisualTree = homeTemplate
            };

            var managingTemplate = new FrameworkElementFactory(typeof(ManagingView));
            managingTemplate.SetValue(DataContextProperty, context.ManagingViewModel);
            Resources["ManagingViewTemplate"] = new DataTemplate
            {
                VisualTree = managingTemplate
            };

            var analyticsTemplate = new FrameworkElementFactory(typeof(AnalyticsControl));
            analyticsTemplate.SetValue(DataContextProperty, context.AnalyticsViewModel);
            Resources["AnalyticsViewTemplate"] = new DataTemplate
            {
                VisualTree = analyticsTemplate
            };
        }
    }
}
