using Microsoft.EntityFrameworkCore;
using TaskList;
using TaskList.ApiHandlers;
using TaskList.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<AppDbContext>(x => x.UseSqlite("Data Source=app.db"))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/task", TaskHandler.GetUserTasksAsync)
    .WithName("List all the tasks for a user")
    .WithOpenApi();

app.MapPost("/task", TaskHandler.CreateTaskAsync)
    .WithName("Creates a new task")
    .WithOpenApi();

app.MapGet("/task/{taskId:guid}", TaskHandler.GetTaskAsync)
    .WithName("Returns task")
    .WithOpenApi();

app.MapPut("/task/{taskId:guid}", TaskHandler.UpdateTaskAsync)
    .WithName("Updates existing task")
    .WithOpenApi();

app.MapDelete("/task/{taskId:guid}", TaskHandler.DeleteTaskAsync)
    .WithName("Deletes the task")
    .WithOpenApi();

app.MapPost("/user/login", UserHandler.Login)
    .WithName("Authenticate the user")
    .WithOpenApi();

app.MapPost("/user/register", UserHandler.Register)
    .WithName("Creates new user")
    .WithOpenApi();

app.Run();