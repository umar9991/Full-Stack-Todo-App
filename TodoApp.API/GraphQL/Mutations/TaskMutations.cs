using HotChocolate;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Infrastructure.Data;
using TaskStatus = TodoApp.Core.Entities.TaskStatus;

namespace TodoApp.API.GraphQL.Mutations;

public class TaskMutations
{
    public async Task<TaskItem> CreateTask(
        string title,
        string description,
        string? status, 
        [Service] AppDbContext context)
    {
        // Determine status: default to Pending
        TaskStatus taskStatus = TaskStatus.Pending;
        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<TaskStatus>(status, true, out var parsed))
        {
            taskStatus = parsed;
        }

        var task = new TaskItem
        {
            Title = title,
            Description = description,
            Status = taskStatus,
            CreatedAt = DateTime.UtcNow
        };

        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        return task;
    }

    // Update only Status
    public async Task<TaskItem> UpdateTaskStatus(
        int id,
        string status,
        [Service] AppDbContext context)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
        {
            throw new GraphQLException($"Task with ID {id} not found.");
        }

        if (Enum.TryParse<TaskStatus>(status, true, out var taskStatus))
        {
            task.Status = taskStatus;
            task.UpdatedAt = DateTime.UtcNow;

            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            return task;
        }

        throw new GraphQLException($"Invalid status: {status}. Valid values are: Pending, Completed");
    }

    // Update Task (title, description, status)
    public async Task<TaskItem> UpdateTask(
        int id,
        string? title,
        string? description,
        string? status,
        [Service] AppDbContext context)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
        {
            throw new GraphQLException($"Task with ID {id} not found.");
        }

        if (!string.IsNullOrEmpty(title))
            task.Title = title;

        if (!string.IsNullOrEmpty(description))
            task.Description = description;

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<TaskStatus>(status, true, out var taskStatus))
            task.Status = taskStatus;

        task.UpdatedAt = DateTime.UtcNow;

        context.Tasks.Update(task);
        await context.SaveChangesAsync();

        return task;
    }

    // Delete Task
    public async Task<bool> DeleteTask(
        int id,
        [Service] AppDbContext context)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
        {
            throw new GraphQLException($"Task with ID {id} not found.");
        }

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();

        return true;
    }
}
