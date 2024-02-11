using TaskList.Models;

namespace TaskList.Repositories;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<User?> GetByName(string username);
    Task<User?> GetById(Guid userId);
}