namespace TodoApp.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public class TaskItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum TaskStatus
{
    Pending,
    Completed
}