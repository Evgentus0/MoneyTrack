using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoneyTrack.Clients.Common.Commands
{
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsync(object parameter);
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public event EventHandler CanExecuteChanged;
    }
}
