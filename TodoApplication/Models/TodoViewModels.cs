using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Models
{
    public class TodoList
    {
        public TodoList()
        {
            
        }

        public TodoList(string title)
        {
            Title = title;
            CreatedTime = DateTime.Now;
        }

        public TodoList(int id, string title)
            : this(title)
        {
            Id = id;
        }

        //public TodoList(int id, string title, List<TodoItem> todoItems)
        //    : this(id, title)
        //{
        //    TodoItems = todoItems;
        //}

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedTime { get; set; }

        //private List<TodoItem> TodoItems { get; }
    }

    public class TodoListsAndTodoList
    {
        public IEnumerable<TodoList> TodoLists { get; set; }

        public TodoList NewTodoList { get; set; }
    }

    public class TodoItems
    {
        public IEnumerable<TodoItem> Items { get; set; }

        public TodoItem NewTodoItem { get; set; }

        public string ListTitle { get; set; }

        public int ListId { get; set; }
    }

    public class TodoItemModel
    {
        public int TodoListId { get; set; }

        public TodoItem TodoItem { get; set; }
    }

    public class TodoItem
    {
        public TodoItem()
        {
            UpdatedTime = DateTime.Now;
        }

        public TodoItem(DateTime createdTime) : this()
        {
            CreatedTime = createdTime != DateTime.MinValue 
                ? createdTime 
                : DateTime.Now;
        }

        public TodoItem(string title, string description, DateTime createdTime, bool isDone = false)
            : this(createdTime)
        {
            Title = title;
            Description = description;
            IsDone = isDone;
        }

        public TodoItem(int id, string title, string description, DateTime createdTime, bool isDone = false)
            : this(title, description, createdTime, isDone)
        {
            Id = id;
        }

        public int Id { get; set; }

        public bool IsDone { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }
    }

    public class Notifier
    {
        public int TodoItemId { get; set; }
        
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime When { get; set; }
    }
}