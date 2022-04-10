using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Data
{
    public interface ICollectionAdapter<T, IdType>
    {
        Task Add(T item);
        Task<T> AddWithSave(T item);
        Task Update(T item);
        Task Remove(IdType id);
        IQueryAdapter<T> Query { get; }
    }
}
