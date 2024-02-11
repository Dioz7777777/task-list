namespace TaskList.Contracts;

public sealed record TaskCreateRequest(string Name, DateOnly DueDate);