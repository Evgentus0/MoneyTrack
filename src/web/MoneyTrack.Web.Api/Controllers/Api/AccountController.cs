using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Web.Api.Models.Entities;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController:MoneyTrackController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AccountModel accountModel)
        {
            var accountDto = _mapper.Map<AccountDto>(accountModel);

            await _accountService.AddAccount(accountDto);

            return Ok();
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var accountsDto = await _accountService.GetAllAccounts();

            var result = _mapper.Map<List<AccountModel>>(accountsDto);

            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] AccountModel accountModel)
        {
            var accountDto = _mapper.Map<AccountDto>(accountModel);

            await _accountService.Update(accountDto);

            return Ok();
        }
    }
}
