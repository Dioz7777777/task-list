using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data.Repositories;

public sealed class TodoItemRepository(ApplicationDbContext context) : ITodoItemRepository
{
    public async Task<TodoItem> Create(TodoItem task)
    {
        context.TodoItems.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<IReadOnlyCollection<TodoItem>> GetByUser(Guid userId) =>
        await context.TodoItems.Where(t => t.UserId == userId).ToArrayAsync();

    public async Task<TodoItem?> GetById(Guid taskId) => await context.TodoItems.FindAsync(taskId);

    public async Task<TodoItem> Update(TodoItem task)
    {
        context.Entry(task).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return task;
    }

    public async Task Delete(Guid id)
    {
        var taskToDelete = await context.TodoItems.FindAsync(id);
        if (taskToDelete != null)
        {
            context.TodoItems.Remove(taskToDelete);
            await context.SaveChangesAsync();
        }
    }
}