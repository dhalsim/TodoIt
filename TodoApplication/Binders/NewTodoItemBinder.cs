using System;
using System.Web;
using System.Web.Mvc;
using TodoApplication.Models;

namespace TodoApplication.Binders
{
    public class NewTodoItemBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            Func<string, string> fg = item => request.Form.Get(item);

            int todoListId;
            int.TryParse(fg("TodoId"), out todoListId);

            return new TodoItemModel
            {
                TodoItem = new Models.TodoItem(fg("NewTodoItem.Title"), fg("NewTodoItem.Description"), DateTime.Now),
                TodoListId = todoListId
            };
        }
    }
}