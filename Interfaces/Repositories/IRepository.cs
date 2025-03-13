using ProvaPub.Models;

namespace ProvaPub.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> SelectByIdAsync(int id);
        Task<bool> ExistAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<T> InsertAsync(T item);
        Task<T?> UpdateAsync(T item); 
        Task<IEnumerable<T>> SelectAllAsync();
        
    }
}
