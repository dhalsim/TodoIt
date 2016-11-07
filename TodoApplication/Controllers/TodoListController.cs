using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TodoApplication.Binders;
using TodoApplication.Models;
using TodoApplication.Repositories;

namespace TodoApplication.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        public TodoListController(ITodoRepository repository)
        {
            _repository = repository;
        }

        private readonly ITodoRepository _repository;

        public ActionResult Index()
        {
            var todos = _repository.GetAllUserTodoLists(User.Identity.GetUserId())
                .Select(tlEntity => new TodoList(tlEntity.Id, tlEntity.Title));

            var todosAndNewTodo = new TodoListsAndTodoList { TodoLists = todos };

            return View(todosAndNewTodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(NewTodoListBinder))]TodoList todoListModel)
        {
            int id = _repository.CreateTodoList(User.Identity.GetUserId(), todoListModel);
            return RedirectToAction("Index", "Todo", new { id });
        }

        public ActionResult Edit(int id)
        {
            var todoList =_repository.GetTodoList(User.Identity.GetUserId(), id);
            return View(new TodoList(todoList.Id, todoList.Title));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoList todoList)
        {
            var userId = User.Identity.GetUserId();

            int todoListId =_repository.UpdateTodoList(
                userId, 
                new Entities.TodoList
                {
                    Id = todoList.Id,
                    Title = todoList.Title,
                    UserId = userId
                }
            );

            return RedirectToAction("Index", "Todo", new { id = todoListId });
        }

        public ActionResult Delete(int id)
        {
            _repository.DeleteTodoList(User.Identity.GetUserId(), id);
            return RedirectToAction("Index", "TodoList");
        }
    }
}