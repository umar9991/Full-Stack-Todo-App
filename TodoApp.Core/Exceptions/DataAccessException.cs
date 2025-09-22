using System;

namespace TodoApp.Core.Exceptions;

public class DataAccessException : Exception
{
    public string EntityType { get; set; }
    public string Operation { get; set; }
    public object EntityId { get; set; }

    public DataAccessException()
    {
    }

    public DataAccessException(string message) : base(message)
    {
    }

    public DataAccessException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public DataAccessException(string message, string entityType, string operation, object entityId = null) 
        : base(message)
    {
        EntityType = entityType;
        Operation = operation;
        EntityId = entityId;
    }

    public DataAccessException(string message, Exception innerException, string entityType, string operation, object entityId = null) 
        : base(message, innerException)
    {
        EntityType = entityType;
        Operation = operation;
        EntityId = entityId;
    }

    public override string ToString()
    {
        return $"DataAccessException: {Message}, EntityType: {EntityType}, Operation: {Operation}, EntityId: {EntityId}\n{base.StackTrace}";
    }
}