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
        
    }
}
