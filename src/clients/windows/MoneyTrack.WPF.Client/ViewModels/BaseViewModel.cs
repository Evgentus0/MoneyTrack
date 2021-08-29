using MoneyTrack.WPF.Client.Models;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public abstract class BaseViewModel: BaseModel
    {
        public abstract Task Initialize();
    }
}
