using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MoneyTrack.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            Data = new HomeModel();
            
            DataContext = Data;

        }
        public HomeModel Data { get; set; }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
