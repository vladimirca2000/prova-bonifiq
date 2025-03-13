using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProvaPub.Repository.Data;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly TestDbContext _context;
    private DbSet<T> _dataset;

    public BaseRepository(TestDbContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
        if (result == null)
            return false;

        _dataset.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistAsync(int id)
    {
        return await _dataset.AnyAsync(P => P.Id.Equals(id));
    }

    public async Task<T> InsertAsync(T item)
    {
        _dataset.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<T>> SelectAllAsync()
    {
        return await _dataset.ToListAsync();
    }

    public async Task<T?> SelectByIdAsync(int id)
    {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        return result;
    }

    public async Task<T?> UpdateAsync(T item)
    {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
        if (result == null)
            return null;

        _context.Entry(result).CurrentValues.SetValues(item);
        await _context.SaveChangesAsync();
        return item;
    }

    //protected readonly TestDbContext _context;

    //private DbSet<T> _dataset;
    //public BaseRepository(TestDbContext context)
    //{
    //    _context = context;
    //    _dataset = _context.Set<T>();
    //}

    //public async Task<T?> SelectByIdAsync(int id)
    //{
    //    return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
    //}

    //public async Task<bool> ExistAsync(int id)
    //{
    //    return await _dataset.AnyAsync(p => p.Id.Equals(id));
    //}

    //public async Task<bool> DeleteAsync(int id)
    //{
    //    var result = await SelectByIdAsync(id);
    //    if (result == null)        {
    //        return false;        }

    //    _dataset.Remove(result);
    //    await _context.SaveChangesAsync();
    //    return true;
    //}

    //public async Task<T> InsertAsync(T item)
    //{       
    //    _dataset.Add(item);
    //    await _context.SaveChangesAsync();
    //    return item;
    //}

    //public async Task<T?> UpdateAsync(T item)
    //{
    //    var result = await SelectByIdAsync(item.Id);
    //    if (result == null)        
    //        return null;        

    //    _dataset.Update(item);
    //    await _context.SaveChangesAsync();
    //    return item;
    //}

    //public async Task<IEnumerable<T>> SelectAllAsync()
    //{
    //    return await _dataset.ToListAsync();
    //}


}
