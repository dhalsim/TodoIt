using System;
using System.Collections.Generic;

namespace TodoApplication.Entities
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}