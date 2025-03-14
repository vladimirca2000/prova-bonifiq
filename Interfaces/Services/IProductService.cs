using ProvaPub.Models;

namespace ProvaPub.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>?> ListProducts(int page);

}
