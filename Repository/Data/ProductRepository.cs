using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;

namespace ProvaPub.Repository.Data;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private DbSet<Product> _dataset;

    public ProductRepository(TestDbContext context) : base(context)
    {
        _dataset = context.Set<Product>();
    }

    public async Task<IEnumerable<Product>> ListProducts(int page, int pageSize)
    {
        var products = await _dataset
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return products;
    }

    public async Task<bool> HasNext(int page, int pageSize)
    {
        var products = await _dataset
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return products.Any();
    }


}
