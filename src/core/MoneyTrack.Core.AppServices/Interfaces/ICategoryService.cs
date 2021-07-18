using MoneyTrack.Core.AppServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
        Task AddCategory(CategoryDto category);
        Task Update(CategoryDto categoryDto);
        Task Delete(int id);
    }
}
