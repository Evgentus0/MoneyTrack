using LiteDB;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Data.LiteDB
{
    public class DbProvider : IDbProvider
    {
        private LiteDatabase _liteDatabase;

        private ICollectionAdapter<Transaction> _transactions;
        public ICollectionAdapter<Transaction> Transactions => _transactions 
            ??= new CollectionAdapter<Transaction>(_liteDatabase.GetCollection<Transaction>());

        private ICollectionAdapter<Category> _categories;
        public ICollectionAdapter<Category> Categories => _categories ??= 
            new CollectionAdapter<Category>(_liteDatabase.GetCollection<Category>());

        private ICollectionAdapter<Account> _accounts;
        public ICollectionAdapter<Account> Accounts => _accounts ??= 
            new CollectionAdapter<Account>(_liteDatabase.GetCollection<Account>());

        public DbProvider(string connectionString)
        {
            var bsonMapper = new BsonMapper();
            bsonMapper.Entity<Transaction>().DbRef(x => x.Account, nameof(Account));
            bsonMapper.Entity<Transaction>().DbRef(x => x.Category, nameof(Category));

            bool isAlreadyExist = File.Exists(connectionString);

            _liteDatabase = new LiteDatabase(connectionString, bsonMapper);

            if (!isAlreadyExist)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            var categories = _liteDatabase.GetCollection<Category>();

            categories.Insert(new Category
            {
                Name = Category.RealBalanceDiff,
                IsSystem = true
            });

            categories.Insert(new Category
            {

                Name = Category.AccChangeCom,
                IsSystem = true
            });
        }

        public void Dispose()
        {
            _liteDatabase.Dispose();
        }
    }
}
