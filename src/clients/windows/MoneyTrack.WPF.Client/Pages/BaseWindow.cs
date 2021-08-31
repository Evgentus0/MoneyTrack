using MoneyTrack.WPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
