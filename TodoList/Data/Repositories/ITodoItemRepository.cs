
using TodoList.Models;

namespace TodoList.Data.Repositories;


public interface ITodoItemRepository
{
    Task<TodoItem> Create(TodoItem task);
    Task<IReadOnlyCollection<TodoItem>> GetByUser(Guid userId);
    Task<TodoItem?> GetById(Guid taskId);
    Task<TodoItem> Update(TodoItem task);
    Task Delete(Guid id);
}