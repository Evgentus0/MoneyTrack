using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategory(CategoryDto category);
        Task Update(CategoryDto categoryDto);
        Task Delete(int id);

        Task<List<CategoryDto>> GetCategories(string userId, List<Filter> filters = null);
    }
}
