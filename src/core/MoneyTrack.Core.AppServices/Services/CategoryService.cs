using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategory(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.IsSystem = false;

            await _categoryRepository.AddCategory(categoryEntity);

            await _categoryRepository.Save();
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.Delete(id);

            await _categoryRepository.Save();
        }

        public async Task<List<CategoryDto>> GetCategories(string userId, List<Filter> filters = null)
        {
            filters = filters ?? new List<Filter>();
            filters.Add(new Filter
            {
                FilterOp = FilterOp.And,
                Operation = Operations.Eq,
                PropName = nameof(Category.User)+nameof(Category.User.Id),
                Value = userId
            });

            List<Category> result = await _categoryRepository.GetCategories(filters);

            return _mapper.Map<List<CategoryDto>>(result);
        }

        public async Task Update(CategoryDto categoryDto)
        {
            Category categoryToUpdate = await _categoryRepository.GetById(categoryDto.Id);

            if (categoryToUpdate != null)
            {
                if (!string.IsNullOrEmpty(categoryDto.Name))
                {
                    categoryToUpdate.Name = categoryDto.Name;
                }

                await _categoryRepository.Update(categoryToUpdate);

                await _categoryRepository.Save();
            }
        }
    }
}
