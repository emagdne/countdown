using System.Data.Entity;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class ToDoItemContext : DbContext
    {
        public ToDoItemContext() : base("SQLite") { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}