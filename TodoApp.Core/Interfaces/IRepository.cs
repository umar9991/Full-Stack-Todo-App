using TodoApp.Core.Entities;

namespace TodoApp.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public interface ITaskRepository : IRepository<TaskItem>
{
    // Additional task-specific methods can be added here
}