using ProvaPub.Models;

namespace ProvaPub.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>?> ListProducts(int page, int pageSize);
    Task<bool> HasNext(int page, int pageSize);
}
