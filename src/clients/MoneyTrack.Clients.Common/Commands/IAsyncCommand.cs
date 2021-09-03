﻿using System.Threading.Tasks;
using System.Windows.Input;

namespace MoneyTrack.Clients.Common.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
