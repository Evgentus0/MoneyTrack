using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MoneyTrack.Core.DomainServices.Exceptions;
using MoneyTrack.Core.DomainServices.Identity;
using MoneyTrack.Core.Models;
using MoneyTrack.Data.MsSqlServer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer.Identity
{
    public class AppUserManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AppUserManager(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> CheckIsAuthenticate(string login, string password)
        {
            var appUser = await _userManager.FindByNameAsync(login);

            if (appUser is not null)
            {
                return await _userManager.CheckPasswordAsync(appUser, password);
            }

            return false;
        }

        public async Task<string> CreateRole(string role)
        {
            var newRole = new IdentityRole
            {
                Name = role,
                NormalizedName = role.ToLower(),
            };

            await _roleManager.CreateAsync(newRole);

            return newRole.Name;
        }

        public async Task<User> CreateUser(User user, string password)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            appUser.Id = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByIdAsync(appUser.Id);
                await _userManager.AddToRolesAsync(newUser, user.Roles);

                return _mapper.Map<User>(newUser);
            }

            throw new MoneyTrackException("Cannot create user");
        }

        public async Task<User> GetByLogin(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(login);
            }

            if(user is null)
            {
                throw new MoneyTrackException($"Cannot find user with login {login}");
            }

            var result = _mapper.Map<User>(user);
            result.Roles = new List<string>(await _userManager.GetRolesAsync(user));

            return result;
        }
    }
}
