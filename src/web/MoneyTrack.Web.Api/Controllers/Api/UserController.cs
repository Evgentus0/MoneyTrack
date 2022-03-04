using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Web.Api.Models.Entities;
using MoneyTrack.Web.Api.Models.Requests;
using MoneyTrack.Web.Api.Models.Responses;
using ServiceModels = MoneyTrack.Core.AppServices.Models;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MoneyTrackController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> SignUp(UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            ServiceModels.SignInResult signInResult = await _userService.SignUp(userDto);
            var response = _mapper.Map<SignInResponse>(signInResult);

            return Ok(response);
        }

        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            ServiceModels.SignInResult result = await _userService.SignIn(request.Login, request.Password);
            var response = _mapper.Map<SignInResponse>(result);

            return Ok(response);
        }
    }
}
