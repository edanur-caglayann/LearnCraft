using LearnCraftt.Application.Repositories;
using LearnCraftt.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LearnCraftt.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    //constructor
    private readonly LearnCrafttDbContext _context;

    public WriteRepository(LearnCrafttDbContext context)
    {
        _context = context;
    }
    
    public DbSet<T> Table => _context.Set<T>();
    
    public async Task<T> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        return entityEntry.Entity;
    }

    public async Task<bool> AddMultipleAsync(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        EntityEntry<T> entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        T entity = await Table.FirstOrDefaultAsync(data => data.Id == id);
        return Delete(entity);
    }

    public bool Delete(T entity)
    {
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool DeleteMultiple(IEnumerable<T> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
    
}