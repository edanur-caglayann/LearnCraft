using LearnCraftt.Domain.Models;

namespace LearnCraftt.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task<bool> AddMultipleAsync(List<T> entities);
    Task<bool> UpdateAsync(T entity); 
    Task<bool> DeleteAsync(Guid id); 
    bool Delete(T entity);
    bool DeleteMultiple(IEnumerable<T> entities);
    Task<int> SaveAsync(); 
}