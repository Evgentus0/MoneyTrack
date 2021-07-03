using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public class UnitOfWork
    {
        public UnitOfWork(AccountRepository accountRepository,
            CategoryRepository categoryRepository,
            TransactionRepository transactionRepository)
        {
            AccountRepository = accountRepository;
            CategoryRepository = categoryRepository;
            TransactionRepository = transactionRepository;
        }

        public AccountRepository AccountRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public TransactionRepository TransactionRepository { get; }
    }
}
