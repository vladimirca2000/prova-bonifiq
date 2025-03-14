using ProvaPub.Models;

namespace ProvaPub.Interfaces.Repositories;

public interface ICustomerRepository: IRepository<Customer>
{
    Task<IEnumerable<Customer>> ListCustumeres(int page, int pageSize);
    Task<bool> HasNext(int page, int pageSize);
}
