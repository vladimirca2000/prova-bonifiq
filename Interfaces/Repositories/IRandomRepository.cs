using ProvaPub.Models;

namespace ProvaPub.Interfaces.Repositories;

public interface IRandomRepository : IRepository<RandomNumber>
{
    Task<IEnumerable<int>> GetRandom();
}
