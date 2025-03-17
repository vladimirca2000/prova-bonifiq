using ProvaPub.Models;

namespace ProvaPub.Interfaces.Repositories;

public interface IOrdemRepository : IRepository<Order>
{
    Task<int> CountAsync(int customerId, DateTime baseDate);
    Task<bool> Purchase(int customerId);

}
