namespace TaskList.Contracts;

public sealed record TaskUpdateRequest(string Name, DateOnly DueDate, bool IsComplete);