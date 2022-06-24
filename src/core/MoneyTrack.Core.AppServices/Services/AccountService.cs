using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(
            AccountRepository accountRepository, 
            IMapper mapper
            )
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task AddAccount(AccountDto account)
        {
            var accountEntity = _mapper.Map<Account>(account);
            accountEntity.LastTransaction = null;

            await _accountRepository.Add(accountEntity);

            await _accountRepository.Save();
        }

        public async Task Delete(int id)
        {
            await _accountRepository.Delete(id);

            await _accountRepository.Save();
        }

        public async Task<List<AccountDto>> GetAccounts(string userId, List<Filter> filters = null)
        {
            filters = filters ?? new List<Filter>();
            filters.Add(new Filter
            {
                FilterOp = FilterOp.And,
                Operation = Operations.Eq,
                PropName = nameof(Account.User)+nameof(Account.User.Id),
                Value = userId
            });

            return _mapper.Map<List<AccountDto>>(await _accountRepository.GetAccounts(filters));
        }

        public async Task Update(AccountDto accountDto, bool addTransaction = false)
        {
            Account accountToUpdate = await _accountRepository.GetById(accountDto.Id);

            if (accountToUpdate != null)
            {
                if (!string.IsNullOrEmpty(accountDto.Name))
                {
                    accountToUpdate.Name = accountDto.Name;
                }

                await _accountRepository.Update(accountToUpdate);

                await _accountRepository.Save();
            }
        }
    }
}
