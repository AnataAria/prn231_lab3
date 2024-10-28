using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class BaseRepository <T,TContext> (TContext context) where T : class where TContext : DbContext {
    protected readonly TContext _context = context;
    protected readonly DbSet<T> _dataset = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dataset.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dataset.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} not found");
        return entity;
    }

    public async Task AddAsync(T entity)
    {
        await _dataset.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dataset.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dataset.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}