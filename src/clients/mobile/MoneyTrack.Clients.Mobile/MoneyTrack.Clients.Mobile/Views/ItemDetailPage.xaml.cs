using MoneyTrack.Clients.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyTrack.Clients.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}