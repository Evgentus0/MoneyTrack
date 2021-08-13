using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
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

        public async Task AddAccount(AccountDto account)
        {
            var accountEntity = _mapper.Map<Account>(account);

            await _accountRepository.Add(accountEntity);
        }

        public async Task Delete(int id)
        {
            await _accountRepository.Delete(id);
        }

        public async Task<List<AccountDto>> GetAllAccounts()
        {
            return _mapper.Map<List<AccountDto>>(await _accountRepository.GetAllAccounts());
        }

        public async Task Update(AccountDto accountDto)
        {
            Account accountToUpdate = await _accountRepository.GetById(accountDto.Id);

            if (accountToUpdate is not null)
            {
                if (!string.IsNullOrEmpty(accountDto.Name))
                {
                    accountToUpdate.Name = accountDto.Name;
                }

                await _accountRepository.Update(accountToUpdate);
            }
        }
    }
}
