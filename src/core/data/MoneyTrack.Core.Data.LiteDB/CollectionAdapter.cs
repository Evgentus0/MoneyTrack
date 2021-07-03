using LiteDB;
using MoneyTrack.Core.DomainServices.Data;
using MoneyTrack.Core.Models;
using MoneyTrack.Core.Models.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.Data.LiteDB
{
    public class CollectionAdapter<T> : ICollectionAdapter<T>
    {
        private readonly ILiteCollection<T> _collection;

        public CollectionAdapter(ILiteCollection<T> collection)
        {
            _collection = collection;
        }

        public IQueryAdapter<T> Query => new QueryAdapter<T>(_collection.Query());

        public async Task Add(T item)
        {
            await Task.Run(() => _collection.Insert(item));
        }

        public async Task Remove(int id)
        {
            await Task.Run(() => _collection.Delete(new BsonValue(id)));
        }

        public async Task Update(T item)
        {
            await Task.Run(() => _collection.Update(item));
        }

    }
}
