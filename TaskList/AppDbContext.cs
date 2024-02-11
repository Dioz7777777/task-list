using Microsoft.EntityFrameworkCore;
using TaskList.Models;
using Task = TaskList.Models.Task;

namespace TaskList;

public sealed class AppDbContext : DbContext
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<Task> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}