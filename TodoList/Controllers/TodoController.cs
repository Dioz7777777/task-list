using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Repositories;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Authorize]
    public sealed class TodoController(UserManager<IdentityUser> userManager, ITodoItemRepository todoItemRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var todos = await todoItemRepository.GetByUser(Guid.Parse(user.Id));

            return View(todos.ToArray());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (!ModelState.IsValid) return View(todoItem);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            todoItem.Id = Guid.NewGuid();
            todoItem.UserId = Guid.Parse(user.Id);
            todoItem.IsComplete = false;

            await todoItemRepository.Create(todoItem);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid todoId)
        {
            var todo = await todoItemRepository.GetById(todoId);
            if (todo == null) RedirectToAction("Index");
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoItem todoItem)
        {
            if (!ModelState.IsValid) return View(todoItem);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            todoItem.UserId = Guid.Parse(user.Id);

            await todoItemRepository.Update(todoItem);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Complete(Guid todoId)
        {
            var todo = await todoItemRepository.GetById(todoId);
            if (todo == null) return RedirectToAction("Index");
            if (todo.IsComplete) return RedirectToAction("Index");

            todo.IsComplete = true;
            await todoItemRepository.Update(todo);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid todoId)
        {
            var todo = await todoItemRepository.GetById(todoId);
            if (todo == null) return RedirectToAction("Index");

            await todoItemRepository.Delete(todoId);

            return RedirectToAction("Index");
        }
    }
}