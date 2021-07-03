using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(AccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<AccountDto>> GetAllAccounts()
        {
            return _mapper.Map<List<AccountDto>>(await _accountRepository.GetAllAccounts());
        }
    }
}
