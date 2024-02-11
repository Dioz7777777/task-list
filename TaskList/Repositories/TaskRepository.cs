using Microsoft.EntityFrameworkCore;
using TaskModel = TaskList.Models.Task;

namespace TaskList.Repositories;

public sealed class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<TaskModel> Create(TaskModel task)
    {
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetByUser(Guid userId) =>
        await context.Tasks.Where(t => t.UserId == userId).ToArrayAsync();

    public async Task<TaskModel?> GetById(Guid taskId) => await context.Tasks.FindAsync(taskId);

    public async Task<TaskModel> Update(TaskModel task)
    {
        context.Entry(task).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return task;
    }

    public async Task Delete(Guid id)
    {
        var taskToDelete = await context.Tasks.FindAsync(id);
        if (taskToDelete != null)
        {
            context.Tasks.Remove(taskToDelete);
            await context.SaveChangesAsync();
        }
    }
}