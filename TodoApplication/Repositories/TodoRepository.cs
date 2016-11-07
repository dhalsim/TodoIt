using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TodoApplication.Data;
using TodoApplication.Entities;

namespace TodoApplication.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _db = new TodoContext();

        public List<TodoList> GetAllUserTodoLists(string userId)
        {
            return _db.TodoLists.Where(tl => tl.UserId == userId).ToList();
        }

        public int CreateTodoList(string userId, Models.TodoList todoListModel)
        {
            var todoListEntity = new TodoList
            {
                Title = todoListModel.Title,
                UserId = userId,
                CreatedTime = todoListModel.CreatedTime
            };

            using (_db)
            {
                _db.TodoLists.Add(todoListEntity);
                _db.SaveChanges();
            }

            return todoListEntity.Id;
        }

        public List<TodoItem> GetTodoItemsOfList(string userId, int todoListId)
        {
            return _db.TodoItems.Where(tdi => tdi.TodoListId == todoListId && tdi.TodoList.UserId == userId).ToList();
        }

        public TodoList GetTodoList(string userId, int id)
        {
            return _db.TodoLists.FirstOrDefault(tl => tl.UserId == userId && tl.Id == id);
        }

        public void CreateTodoItem(string userId, int todoListId, Models.TodoItem todoItem)
        {
            var todoList = GetTodoList(userId, todoListId);

            if (todoList == null)
            {
                throw new ApplicationException("It's not your todo!");
            }

            var todoItemEntity = new TodoItem
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                CreatedTime = todoItem.CreatedTime,
                UpdatedTime = todoItem.UpdatedTime,
                TodoList = todoList
            };

            using (_db)
            {
                _db.TodoItems.Add(todoItemEntity);
                _db.SaveChanges();
            }
        }

        public void DeleteTodoList(string userId, int id)
        {
            var todoList = GetTodoList(userId, id);

            if (todoList == null)
            {
                throw new ApplicationException("It's not your todo!");
            }

            using (_db)
            {
                _db.TodoLists.Remove(todoList);
                _db.SaveChanges();
            }
        }

        public int UpdateTodoList(string userId, TodoList todoList)
        {
            var oldList = GetTodoList(userId, todoList.Id);

            if (oldList == null)
            {
                throw new ApplicationException("It's not your todo!");
            }

            using (_db)
            {
                _db.TodoLists.Attach(oldList);
                oldList.Title = todoList.Title;
                _db.SaveChanges();
            }

            return todoList.Id;
        }

        public TodoList GetTodoListOfItem(string userId, int id)
        {
            var todoItem = _db.TodoItems.FirstOrDefault(ti => ti.Id == id);
            if (todoItem == null)
            {
                throw new ApplicationException("Can't find todo!");
            }

            if (todoItem.TodoList.UserId == userId)
            {
                return todoItem.TodoList;
            }

            throw new ApplicationException("It's not your todo!");
        }

        public int DeleteTodo(string userId, int id)
        {
            var todoItem = GetTodo(userId, id);

            if (todoItem == null)
            {
                throw new ApplicationException("It's not your todo!");
            }
            
            using (_db)
            {
                _db.TodoItems.Remove(todoItem);
                _db.SaveChanges();
            }

            return todoItem.TodoListId;
        }

        public int UpdateTodo(string userId, TodoItem todoItem)
        {
            var todoList = GetTodoListOfItem(userId, todoItem.Id);

            if (todoList == null)
            {
                throw new ApplicationException("It's not your todo!");
            }

            var oldItem = GetTodo(todoItem.Id);

            if (oldItem == null)
            {
                throw new ApplicationException("Can't find todo!");
            }

            using (_db)
            {
                _db.TodoItems.Attach(oldItem);

                oldItem.Title = todoItem.Title;
                oldItem.Description = todoItem.Description;
                oldItem.IsDone = todoItem.IsDone;
                oldItem.UpdatedTime = DateTime.Now;

                _db.SaveChanges();
            }

            return todoList.Id;
        }

        public TodoItem GetTodo(string userId, int id)
        {
            var todoList = GetTodoListOfItem(userId, id);

            if (todoList == null)
            {
                throw new ApplicationException("It's not your todo!");
            }

            return GetTodo(id);
        }

        private TodoItem GetTodo(int id)
        {
            return _db.TodoItems.FirstOrDefault(ti => ti.Id == id);
        }
    }
}