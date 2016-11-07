using System;

namespace TodoApplication.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int TodoListId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public virtual TodoList TodoList { get; set; }
    }
}