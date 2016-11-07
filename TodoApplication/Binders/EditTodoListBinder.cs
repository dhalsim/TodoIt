using System;
using System.Web;
using System.Web.Mvc;

namespace TodoApplication.Binders
{
    //public class EditTodoListBinder : IModelBinder
    //{
    //    public object BindModel(ControllerContext controllerContext,
    //                            ModelBindingContext bindingContext)
    //    {
    //        HttpRequestBase request = controllerContext.HttpContext.Request;

    //        Func<string, string> fg = item => request.Form.Get(item);

    //        int todoListId;
    //        int.TryParse(fg("Id"), out todoListId);

    //        return new Models.TodoList(todoListId, fg("Title"));
    //    }
    //}
}