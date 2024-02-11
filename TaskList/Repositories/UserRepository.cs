using Microsoft.EntityFrameworkCore;
using TaskList.Models;

namespace TaskList.Repositories;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> Create(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public Task<User?> GetByName(string username) => context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public Task<User?> GetById(Guid userId) => context.Users.FirstOrDefaultAsync(u => u.Id == userId);
}