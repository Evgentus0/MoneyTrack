using MoneyTrack.Clients.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Clients.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewTransactionPage : ContentPage
    {
        public AddNewTransactionPage()
        {
            InitializeComponent();
            // need to add sql lite db
            var viewModel = App.GetViewModel<HomeViewModel>();

            BindingContext = viewModel;
            Task.Run(viewModel.Initialize);
        }
    }
}