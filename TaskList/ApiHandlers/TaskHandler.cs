using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Repositories;
using Task = TaskList.Models.Task;

namespace TaskList.ApiHandlers;

public static class TaskHandler
{
    public static async Task<IResult> GetUserTasksAsync(ITaskRepository taskRepository, [FromHeader] Guid userId)
    {
        var tasks = await taskRepository.GetByUser(userId);
        return Results.Ok(tasks);
    }

    public static async Task<IResult> CreateTaskAsync(
        TaskCreateRequest request, IUserRepository userRepository, ITaskRepository taskRepository, [FromHeader] Guid userId)
    {
        var user = await userRepository.GetById(userId);
        if (user == null) return Results.Unauthorized();

        var task = new Task
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = request.Name,
            DueDate = request.DueDate,
            IsComplete = false
        };
        await taskRepository.Create(task);

        return Results.Created();
    }

    public static async Task<IResult> GetTaskAsync(IUserRepository userRepository, ITaskRepository taskRepository, [FromHeader] Guid userId, Guid taskId)
    {
        var user = await userRepository.GetById(userId);
        if (user == null) return Results.Unauthorized();

        var task = await taskRepository.GetById(taskId);
        if (task == null) return Results.NotFound("Task not found");

        if (task.UserId != userId) return Results.Forbid();

        return Results.Ok(task);
    }

    public static async Task<IResult> UpdateTaskAsync(
        IUserRepository userRepository, ITaskRepository taskRepository, [FromHeader] Guid userId, Guid taskId, TaskUpdateRequest request)
    {
        var user = await userRepository.GetById(userId);
        if (user == null) return Results.Unauthorized();

        var task = await taskRepository.GetById(taskId);
        if (task == null) return Results.NotFound("Task not found");

        if (task.UserId != userId) return Results.Forbid();

        task.Name = request.Name;
        task.DueDate = request.DueDate;
        task.IsComplete = request.IsComplete;
        await taskRepository.Update(task);

        return Results.Ok(task);
    }

    public static async Task<IResult> DeleteTaskAsync(IUserRepository userRepository, ITaskRepository taskRepository, [FromHeader] Guid userId, Guid taskId)
    {
        var user = await userRepository.GetById(userId);
        if (user == null) return Results.Unauthorized();

        var task = await taskRepository.GetById(taskId);
        if (task == null) return Results.NotFound("Task not found");

        if (task.UserId != userId) return Results.Forbid();

        await taskRepository.Delete(taskId);

        return Results.NoContent();
    }
}