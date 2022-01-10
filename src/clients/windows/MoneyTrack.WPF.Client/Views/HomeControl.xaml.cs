using MoneyTrack.Clients.Common.ViewModels;
using MoneyTrack.WPF.Client.Pages;
using System.Text.RegularExpressions;

namespace MoneyTrack.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : BaseUserControl
    {
        public HomeControl()
        {
            InitializeComponent();
        }

        private void NumberTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var onlyNumbersAndPointRegex = new Regex("[\\d | \\. | -]");

            e.Handled = !onlyNumbersAndPointRegex.IsMatch(e.Text);
        }

        private void DetailedHistoryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new TransactionListWindow();
            window.DataContext = ((HomeViewModel)DataContext).TransactionListViewModel;

            window.ShowDialog();

            ((HomeViewModel)DataContext).SetTransactions();
        }
    }
}
