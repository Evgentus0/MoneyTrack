using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.AppServices.Models;
using MoneyTrack.Core.DomainServices.Exceptions;
using MoneyTrack.Core.DomainServices.Identity;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<SignInResult> SignIn(string login, string password)
        {
            bool isAuthenticate = await _userManager.CheckIsAuthenticate(login, password);

            if (isAuthenticate)
            {
                User user = await _userManager.GetByLogin(login);
                var userDto = _mapper.Map<UserDto>(user);

                return GenerateResult(userDto);
            }

            throw new MoneyTrackException("Login or/and password is incorrect");
        }

        public async Task<SignInResult> SignUp(UserDto userDto, string password)
        {
            var user = _mapper.Map<User>(userDto);
            user = await _userManager.Create(user, password);

            userDto = _mapper.Map<UserDto>(user);

            return GenerateResult(userDto);
        }

        private SignInResult GenerateResult(UserDto userDto)
        {
            throw new NotImplementedException();
        }

    }
}
