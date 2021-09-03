using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public class AnalyticsViewModel : BaseViewModel
    {
        public override string this[string columnName] => string.Empty;

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}
