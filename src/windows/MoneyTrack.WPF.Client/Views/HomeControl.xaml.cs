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
            var onlyNumbersAndPointRegex = new Regex("[\\d | \\.]");

            e.Handled = !onlyNumbersAndPointRegex.IsMatch(e.Text);
        }
    }
}
