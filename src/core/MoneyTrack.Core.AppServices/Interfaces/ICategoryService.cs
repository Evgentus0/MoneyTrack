using MoneyTrack.Core.AppServices.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
