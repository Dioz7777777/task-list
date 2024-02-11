namespace TaskList.Models;

public sealed record User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
}