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
    }
}
