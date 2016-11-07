using System;
using System.Web;
using System.Web.Mvc;

namespace TodoApplication.Binders
{
    public class NewTodoListBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            return new Models.TodoList(request.Form.Get("NewTodoList.Title"));
        }
    }
}