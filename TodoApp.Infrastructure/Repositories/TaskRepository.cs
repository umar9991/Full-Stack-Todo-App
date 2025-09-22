using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Core.Entities;
using TodoApp.Core.Exceptions;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<TaskRepository> _logger;

    public TaskRepository(AppDbContext context, ILogger<TaskRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaskItem> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting task by ID: {TaskId}", id);
        
        try
        {
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
            {
                _logger.LogWarning("Task with ID {TaskId} not found", id);
            }
            else
            {
                _logger.LogDebug("Successfully retrieved task with ID: {TaskId}", id);
            }
            
            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting task by ID: {TaskId}", id);
            throw new DataAccessException(
                $"Failed to retrieve task with ID {id}", 
                ex, 
                nameof(TaskItem), 
                "Read", 
                id
            );
        }
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
    {
        _logger.LogDebug("Getting all tasks");
        
        try
        {
            var tasks = await _context.Tasks.ToListAsync();
            _logger.LogDebug("Successfully retrieved {Count} tasks", tasks.Count);
            return tasks;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tasks");
            throw new DataAccessException(
                "Failed to retrieve tasks", 
                ex, 
                nameof(TaskItem), 
                "Read"
            );
        }
    }

    public async Task<TaskItem> AddAsync(TaskItem entity)
    {
        _logger.LogDebug("Adding new task: {TaskTitle}", entity.Title);
        
        try
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Successfully added task with ID: {TaskId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding task: {TaskTitle}", entity.Title);
            throw new DataAccessException(
                $"Failed to create task '{entity.Title}'", 
                ex, 
                nameof(TaskItem), 
                "Create"
            );
        }
    }

    public async Task UpdateAsync(TaskItem entity)
    {
        _logger.LogDebug("Updating task with ID: {TaskId}", entity.Id);
        
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Successfully updated task with ID: {TaskId}", entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task with ID: {TaskId}", entity.Id);
            throw new DataAccessException(
                $"Failed to update task with ID {entity.Id}", 
                ex, 
                nameof(TaskItem), 
                "Update", 
                entity.Id
            );
        }
    }

    public async Task DeleteAsync(TaskItem entity)
    {
        _logger.LogDebug("Deleting task with ID: {TaskId}", entity.Id);
        
        try
        {
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Successfully deleted task with ID: {TaskId}", entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task with ID: {TaskId}", entity.Id);
            throw new DataAccessException(
                $"Failed to delete task with ID {entity.Id}", 
                ex, 
                nameof(TaskItem), 
                "Delete", 
                entity.Id
            );
        }
    }
}