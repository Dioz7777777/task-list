using TaskModel = TaskList.Models.Task;

namespace TaskList.Repositories;


public interface ITaskRepository
{
    Task<TaskModel> Create(TaskModel task);
    Task<IReadOnlyCollection<TaskModel>> GetByUser(Guid userId);
    Task<TaskModel?> GetById(Guid taskId);
    Task<TaskModel> Update(TaskModel task);
    Task Delete(Guid id);
}