using MoneyTrack.WPF.Client.Models;

namespace MoneyTrack.WPF.Client.ViewModels
{
    public abstract class BaseViewModel: BaseModel
    {
        public abstract void Initialize();
    }
}
