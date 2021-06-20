using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.DomainServices.DbProvider;
using System.Collections.Generic;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LiteDbProvider _dbProvider;

        public CategoryRepository(LiteDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public List<Category> GetAllCategories()
        {
            using var db = _dbProvider.GetDb();

            var collection = db.GetCollection<Category>();

            return collection.Query().ToList();
        }
    }
}
