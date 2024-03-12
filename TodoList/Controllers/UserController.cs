using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Dtos;

namespace TodoList.Controllers;

public sealed class UserController(HttpClient client, IConfiguration configuration) : Controller
{
    public async Task<IActionResult> Index()
    {
        var accessToken = TokenStorage.GoogleAccessToken;
        if (string.IsNullOrEmpty(accessToken)) return Unauthorized();

        var url = configuration.GetSection("Authentication:Google.UserInfoEndpoint").Value;
        if (string.IsNullOrEmpty(url)) return BadRequest();

        var response = await client.GetAsync(string.Format(url, accessToken));

        if (!response.IsSuccessStatusCode) return Unauthorized();

        var data = await response.Content.ReadFromJsonAsync<GetGoogleUserInfoResponse>();


        return View(data);
    }
}