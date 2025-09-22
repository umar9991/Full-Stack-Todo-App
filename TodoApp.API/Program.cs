using Microsoft.EntityFrameworkCore;
using TodoApp.API.GraphQL.Mutations;
using TodoApp.API.GraphQL.Queries;
using TodoApp.API.GraphQL.Types;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=todoapp.db";
builder.Services.AddInfrastructure(connectionString);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<TaskQueries>()       
    .AddMutationType<TaskMutations>()   
    .AddType<TaskType>()                
    .AddType<TaskStatusType>()           
    .AddFiltering()
    .AddSorting()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseCors("AllowReactApp");

app.MapGraphQL();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        if (!await context.Tasks.AnyAsync())
        {
            context.Tasks.AddRange(
                new TodoApp.Core.Entities.TaskItem
                {
                    Title = "Sample Task 1",
                    Description = "This is a sample task",
                    Status = TodoApp.Core.Entities.TaskStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                },
                new TodoApp.Core.Entities.TaskItem
                {
                    Title = "Sample Task 2", 
                    Description = "Another sample task",
                    Status = TodoApp.Core.Entities.TaskStatus.Completed,
                    CreatedAt = DateTime.UtcNow
                }
            );
            await context.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

app.Run();