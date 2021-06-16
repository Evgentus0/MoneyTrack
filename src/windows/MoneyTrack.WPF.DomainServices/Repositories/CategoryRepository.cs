using LiteDB;
using MoneyTrack.Core.DomainServices.Repositories;
using MoneyTrack.Core.Models;
using MoneyTrack.WPF.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.WPF.DomainServices.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppSettings _settings;

        public CategoryRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public List<Category> GetAllCategories()
        {
            using var db = new LiteDatabase(_settings.LiteDBConnection);

            var collection = db.GetCollection<Category>();

            return collection.Query().ToList();
        }
    }
}
