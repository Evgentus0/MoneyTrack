using AutoMapper;
using MoneyTrack.Core.AppServices.Automapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Services;
using MoneyTrack.Core.AppServices.Tests.Repositories;
using MoneyTrack.Core.Models;

namespace MoneyTrack.Core.AppServices.Tests.Scenarios
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private TransactionRepository _transactionRepository;
        private IMapper _mapper;
        private AccountRepository _accountRepository;

        [SetUp]
        public void SetUp()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new DomainModelsDtoMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _transactionRepository = new TransactionRepository();
            _accountRepository = new AccountRepository();
        }

        [Test]
        [TestCaseSource(typeof(Data.Data.TransactionData), nameof(Data.Data.TransactionData.UpdateCases))]
        public void UpdateTest(List<Transaction> initialTransactions,
            TransactionDto transactionToUpdate, List<Transaction> expectedTransaction)
        {
            // Arrange
            _transactionRepository.Transactions = initialTransactions;
            var transactionService = new TransactionService(_transactionRepository, _accountRepository, _mapper);

            // Act
            transactionService.Update(transactionToUpdate).Wait();

            // Assert
            var updatedTransaction = _transactionRepository.Transactions;

            CollectionAssert.AreEqual(expectedTransaction.OrderBy(x => x.Id), updatedTransaction.OrderBy(x => x.Id));
        }
    }
}
