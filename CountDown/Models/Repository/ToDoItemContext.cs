using System.Data.Entity;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    public class ToDoItemContext : DbContext
    {
        public ToDoItemContext() : base("MySql") { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}