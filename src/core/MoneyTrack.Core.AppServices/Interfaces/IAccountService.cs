using MoneyTrack.Core.AppServices.DTOs;
using System.Collections.Generic;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface IAccountService
    {
        List<AccountDto> GetAllAccounts();
    }
}
