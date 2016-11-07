using System.Collections.Generic;
using TodoApplication.Entities;

namespace TodoApplication.Repositories
{
    public interface ITodoRepository
    {
        List<TodoList> GetAllUserTodoLists(string userId);

        int CreateTodoList(string userId, Models.TodoList todoList);

        List<TodoItem> GetTodoItemsOfList(string userId, int todoListId);

        TodoList GetTodoList(string userId, int id);

        void CreateTodoItem(string userId, int todoListId, Models.TodoItem todoItem);

        void DeleteTodoList(string userId, int id);

        int UpdateTodoList(string userId, TodoList todoList);

        TodoList GetTodoListOfItem(string userId, int id);

        int DeleteTodo(string userId, int id);

        int UpdateTodo(string userId, TodoItem todoItem);

        TodoItem GetTodo(string getUserId, int id);
    }
}