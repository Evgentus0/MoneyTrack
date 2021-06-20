using MoneyTrack.Core.Models;
using System.Collections.Generic;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}
