using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;

namespace ProvaPub.Repository.Data
{
    public class RandomRepository : BaseRepository<RandomNumber>, IRandomRepository
    {
        private DbSet<RandomNumber> _dataset;
        public RandomRepository(TestDbContext context) : base(context)
        {
            _dataset = context.Set<RandomNumber>();
        }

        public async Task<IEnumerable<int>> GetRandom()
        {
            var result = await _dataset.ToListAsync();
            return result.Select(x => x.Number);
        }
    }
}
