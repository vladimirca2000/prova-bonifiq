using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;

namespace ProvaPub.Repository.Data
{
    public class OrdemRepository : BaseRepository<Order>, IOrdemRepository
    {
        private DbSet<Order> _dataset;

        public OrdemRepository(TestDbContext context) : base(context)
        {
            _dataset = context.Set<Order>();
        }

        public Task<int> CountAsync(int customerId, DateTime baseDate)
        {
            var numeroDeOrdemMes = _dataset.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            return numeroDeOrdemMes;
        }

        public async Task<bool> Purchase(int customerId)
        {

            var Comprou = await _dataset.AnyAsync(s => s.CustomerId.Equals(customerId));
            return Comprou;

        }
    }
}
