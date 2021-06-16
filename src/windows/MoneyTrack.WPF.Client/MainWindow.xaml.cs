using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.ViewModels;
using MoneyTrack.WPF.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            var analyticsTemplate = new FrameworkElementFactory(typeof(AnalyticsControl));
            analyticsTemplate.SetValue(DataContextProperty, context.AnalyticsViewModel);
            Resources["AnalyticsViewTemplate"] = new DataTemplate
            {
                VisualTree = analyticsTemplate
            };
        }
    }
}
