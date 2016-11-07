using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TodoApplication.Binders;
using TodoApplication.Models;
using TodoApplication.Repositories;
using TodoApplication.Services;

namespace TodoApplication.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        public TodoController(ITodoRepository repository, INotifierService notifierService)
        {
            _repository = repository;
            _notifierService = notifierService;
        }

        private readonly ITodoRepository _repository;
        private readonly INotifierService _notifierService;

        public ActionResult Index(int id)
        {
            var userId = User.Identity.GetUserId();

            var todoItems =_repository.GetTodoItemsOfList(userId, id)
                .ToList();

            var title = todoItems.Count == 0 
                ? _repository.GetTodoList(userId, id).Title 
                : todoItems.FirstOrDefault()?.TodoList.Title;

            var model = new TodoItems
            {
                Items = todoItems.Select(ti => new TodoItem(ti.Id, ti.Title, ti.Description, DateTime.Now, ti.IsDone)),
                ListTitle = title,
                ListId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(NewTodoItemBinder))]TodoItemModel todoItemModel)
        {
            var userId = User.Identity.GetUserId();
            var todoListId = todoItemModel.TodoListId;
            _repository.CreateTodoItem(userId, todoListId, todoItemModel.TodoItem);

            return RedirectToAction("Index", "Todo", new { id = todoListId });
        }

        public ActionResult Edit(int id)
        {
            var todoItem = _repository.GetTodo(User.Identity.GetUserId(), id);
            return View(new Models.TodoItem
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsDone = todoItem.IsDone
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoItem todoItem)
        {
            var userId = User.Identity.GetUserId();

            int todoListId = _repository.UpdateTodo(
                userId,
                new Entities.TodoItem
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Description = todoItem.Description,
                    IsDone = todoItem.IsDone,
                    UpdatedTime = DateTime.Now
                }
            );

            return RedirectToAction("Index", "Todo", new { id = todoListId });
        }

        public ActionResult AddNotifier(int id)
        {
            var todoItem = _repository.GetTodo(User.Identity.GetUserId(), id);

            return View(new Notifier { TodoItemId = todoItem.Id, When = DateTime.Now.AddDays(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNotifier(Notifier notifier)
        {
            var userId = User.Identity.GetUserId();
            var todoItem = _repository.GetTodo(userId, notifier.TodoItemId);

            await _notifierService.SendMessage("Done.It", notifier.Email, "Notification",
                string.Format($"Title: {todoItem.Title}, Message: {todoItem.Description}"), notifier.When);

            return RedirectToAction("Index", "Todo", new { id = todoItem.TodoListId });
        }

        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            int todoListId =_repository.DeleteTodo(userId, id);
            return RedirectToAction("Index", "Todo", new { id = todoListId });

        }
    }
}