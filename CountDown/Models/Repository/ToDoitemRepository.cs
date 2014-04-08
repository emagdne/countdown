using System.Linq;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    public interface IToDoItemRepository
    {
        void InsertToDo(ToDoItem item);
        IQueryable<ToDoItem> AllToDoItems();
        void SaveChanges();
    }

    public class ToDoItemRepository : IToDoItemRepository
    {
        private ToDoItemContext _db;

        public ToDoItemRepository()
        {
            _db = new ToDoItemContext();
        }

        public ToDoItemRepository(ToDoItemContext context)
        {
            _db = context;
        }

        public void InsertToDo(ToDoItem item)
        {
            _db.ToDoItems.Add(item);
        }

        public IQueryable<ToDoItem> AllToDoItems()
        {
            return _db.ToDoItems;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}