using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface IUserService
    {
        Task<SignInResult> SignUp(UserDto userDto, string password);
        Task<SignInResult> SignIn(string login, string password);
    }
}
