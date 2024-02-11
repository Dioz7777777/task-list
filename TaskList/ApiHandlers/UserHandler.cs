using TaskList.Contracts;
using TaskList.Models;
using TaskList.Repositories;

namespace TaskList.ApiHandlers;

public static class UserHandler
{
    public static async Task<IResult> Register(IUserRepository userRepository, UserRegisterRequest userRegisterRequest)
    {
        if (string.IsNullOrWhiteSpace(userRegisterRequest.UserName))
            return Results.BadRequest("Username is required");
        if (string.IsNullOrWhiteSpace(userRegisterRequest.Password))
            return Results.BadRequest("Password is required");
        if (string.IsNullOrWhiteSpace(userRegisterRequest.FullName))
            return Results.BadRequest("Full name is required");

        var existingUser = await userRepository.GetByName(userRegisterRequest.UserName);
        if (existingUser != null)
            return Results.Conflict("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userRegisterRequest.UserName,
            PasswordHash = PasswordHelper.HashPassword(userRegisterRequest.Password),
            FullName = userRegisterRequest.FullName
        };

        await userRepository.Create(user);

        return Results.Ok("User created");
    }

    public static async Task<IResult> Login(HttpContext context, IUserRepository userRepository,  UserLoginRequest userLoginRequest)
    {
        if (string.IsNullOrWhiteSpace(userLoginRequest.UserName))
            return Results.BadRequest("Username is required");
        if (string.IsNullOrWhiteSpace(userLoginRequest.Password))
            return Results.BadRequest("Password is required");

        var user = await userRepository.GetByName(userLoginRequest.UserName);
        if (user == null)
            return Results.NotFound("User doesn't exists");
        if (!PasswordHelper.VerifyPassword(userLoginRequest.Password, user.PasswordHash))
            return Results.Unauthorized();

        context.Response.Cookies.Append("userId", user.Id.ToString());

        return Results.Ok();
    }
}