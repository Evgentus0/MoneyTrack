using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
