using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;

namespace ProvaPub.Repository.Data
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private DbSet<Customer> _dataset;
        public CustomerRepository(TestDbContext context) : base(context)
        {
            _dataset = context.Set<Customer>();
        }

        public async Task<IEnumerable<Customer>> ListCustumeres(int page, int pageSize)
        {
            var customers = await _dataset
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return customers;
        }

        public async Task<bool> HasNext(int page, int pageSize)
        {
            var customers = await _dataset
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return customers.Any();
        }

    }
}
