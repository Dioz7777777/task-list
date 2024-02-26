using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers;

public class HomeController(SignInManager<IdentityUser> signInManager) : Controller
{
    public IActionResult Index() => signInManager.IsSignedIn(User) ? RedirectToAction("Index", "Todo") : View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}