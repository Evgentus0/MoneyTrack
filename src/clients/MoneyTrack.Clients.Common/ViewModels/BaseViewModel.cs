using MoneyTrack.Clients.Common.Models;
using System.Threading.Tasks;

namespace MoneyTrack.Clients.Common.ViewModels
{
    public abstract class BaseViewModel: BaseModel
    {
        public abstract Task Initialize();

        public virtual string Title { get; set; } = "Default";
    }
}
