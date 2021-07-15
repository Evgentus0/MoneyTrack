using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public class CategoryRepository
    {
        private readonly IDbProvider _dbProvider;

        public CategoryRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dbProvider.Categories.Query.ToList();
        }

        public async Task AddCategory(Category categoryEntity)
        {
            await _dbProvider.Categories.Add(categoryEntity);
        }
    }
}
