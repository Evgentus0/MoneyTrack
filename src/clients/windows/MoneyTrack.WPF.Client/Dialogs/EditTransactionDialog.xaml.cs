using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MoneyTrack.WPF.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for EditTransactionDialog.xaml
    /// </summary>
    public partial class EditTransactionDialog : UserControl
    {
        public EditTransactionDialog()
        {
            InitializeComponent();
        }

        private void NumberTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var onlyNumbersAndPointRegex = new Regex("[\\d | \\. | -]");

            e.Handled = !onlyNumbersAndPointRegex.IsMatch(e.Text);
        }
    }
}
