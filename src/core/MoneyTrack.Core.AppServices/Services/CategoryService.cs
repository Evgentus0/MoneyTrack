using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.DomainServices.Repositories;
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

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetAllCategories());
        }
    }
}
