using HotChocolate.Types;
using TodoApp.Core.Entities;
using TaskStatus = TodoApp.Core.Entities.TaskStatus; 

namespace TodoApp.API.GraphQL.Types;

public class TaskType : ObjectType<TaskItem>
{
    protected override void Configure(IObjectTypeDescriptor<TaskItem> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IntType>>();
        descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.Description).Type<StringType>();
        descriptor.Field(t => t.Status).Type<NonNullType<EnumType<TaskStatus>>>(); 
        descriptor.Field(t => t.CreatedAt).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.UpdatedAt).Type<DateTimeType>();
    }
}

public class TaskStatusType : EnumType<TaskStatus> 
{
    protected override void Configure(IEnumTypeDescriptor<TaskStatus> descriptor)
    {
        descriptor.Name("TaskStatus");
        descriptor.Value(TaskStatus.Pending).Name("PENDING");
        descriptor.Value(TaskStatus.Completed).Name("COMPLETED");
    }
}