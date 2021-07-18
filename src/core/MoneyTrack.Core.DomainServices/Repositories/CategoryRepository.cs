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

        public async Task Delete(int id)
        {
            await _dbProvider.Categories.Remove(id);
        }

        public async Task Update(Category category)
        {
            var existingCategory = await _dbProvider.Categories.Query.Where(new Models.Operational.Filter
            {
                PropName = nameof(category.Id),
                Operation = Models.Operational.Operations.Eq,
                Value = category.Id.ToString()
            }).First();

            if(existingCategory is not null)
            {
                if (!string.IsNullOrEmpty(category.Name))
                {
                    existingCategory.Name = category.Name;
                }

                await _dbProvider.Categories.Update(existingCategory);
            }
        }
    }
}
