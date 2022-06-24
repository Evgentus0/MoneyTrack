using MoneyTrack.Core.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> SignUp(UserDto userDto, string password);
        Task<UserDto> SignIn(string login, string password);
        Task<UserDto> AddRole(string userId, string role);
        Task<UserDto> GetByUsername(string username);
    }
}
