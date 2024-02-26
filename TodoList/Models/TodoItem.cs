namespace TodoList.Models;

public sealed class TodoItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateOnly DueDate { get; set; }
    public bool IsComplete { get; set; }
}