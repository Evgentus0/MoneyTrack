using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.Client.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _command;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(Func<object, Task> command, Func<object, bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command(parameter);
        }
    }
}
