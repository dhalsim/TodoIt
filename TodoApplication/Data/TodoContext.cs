using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TodoApplication.Entities;

namespace TodoApplication.Data
{
    public class TodoContext : DbContext
    {

        public TodoContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TodoContext>());
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}