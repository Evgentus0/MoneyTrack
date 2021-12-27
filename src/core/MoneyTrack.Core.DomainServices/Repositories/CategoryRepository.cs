using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
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

        public async Task Delete(int id)
        {
            await _dbProvider.Categories.Remove(id);
        }

        public async Task Update(Category category)
        {
            await _dbProvider.Categories.Update(category);
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbProvider.Categories.Query.Where(new Filter
            {
                PropName = nameof(id),
                Operation = Operations.Eq,
                Value = id.ToString()
            }).First();
        }

        public async Task<List<Category>> GetCategories(List<Filter> filters)
        {
            var categories = _dbProvider.Categories.Query;

            categories = categories.Where(filters);

            return await categories.ToList();
        }
    }
}
