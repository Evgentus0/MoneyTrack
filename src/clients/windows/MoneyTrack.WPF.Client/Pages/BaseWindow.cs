using MoneyTrack.Clients.Common.ViewModels;
using System.Windows;

namespace MoneyTrack.WPF.Client.Pages
{
    public class BaseWindow: Window
    {
        public BaseWindow()
        {
            Loaded += BaseWindow_Loaded;
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseViewModel context)
                //Task.Run(async () => await context?.Initialize());
                context.Initialize();
        }
    }
}
