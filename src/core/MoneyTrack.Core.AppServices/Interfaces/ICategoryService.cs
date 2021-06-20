using MoneyTrack.Core.AppServices.DTOs;
using System.Collections.Generic;

namespace MoneyTrack.Core.AppServices.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories();
    }
}
