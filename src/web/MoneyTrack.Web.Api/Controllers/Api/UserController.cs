using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Data.MsSqlServer.Identity;
using MoneyTrack.Web.Api.Models.Entities;
using MoneyTrack.Web.Api.Models.Requests;
using MoneyTrack.Web.Api.Models.Responses;
using MoneyTrack.Web.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MoneyTrackController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UserController(IUserService userService, 
            IMapper mapper, 
            ITokenGenerator tokenGenerator,
            JwtSecurityTokenHandler tokenHandler)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _tokenHandler = tokenHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var userDto = _mapper.Map<UserDto>(request.User);
            userDto.Roles = new List<string> { UserRoles.User };

            userDto = await _userService.SignUp(userDto, request.Passwords);
            var response = GetResponse(userDto);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var userDto = await _userService.SignIn(request.Login, request.Password);
            var response = GetResponse(userDto);

            return Ok(response);
        }

        private SignInResponse GetResponse(UserDto user)
        {
            var token = _tokenGenerator.GenerateToken(user);

            return new SignInResponse
            {
                Token = _tokenHandler.WriteToken(token),
                ExpiredAt = token.ValidTo
            };
        }
    }
}
