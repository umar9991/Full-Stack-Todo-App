using HotChocolate;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.API.GraphQL.Queries;

public class TaskQueries
{
    public async Task<List<TaskItem>> GetAllTasks([Service] AppDbContext context)
    {
        return await context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetTaskById(int id, [Service] AppDbContext context)
    {
        return await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }
}