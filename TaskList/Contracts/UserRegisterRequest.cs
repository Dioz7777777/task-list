namespace TaskList.Contracts;

public sealed record UserRegisterRequest(string UserName, string Password, string FullName);