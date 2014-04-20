using System.Collections.Generic;
using System.Linq;
using CountDown.Models.Domain;
using MvcPaging;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public interface IToDoItemRepository
    {
        void InsertToDo(ToDoItem item);
        IQueryable<ToDoItem> AllToDoItems();
        ToDoItem FindById(int id);
        int ToDoItemsCount();
        int PendingToDoItemsCount(int userId);
        IPagedList<ToDoItem> GetPagedToDoItems(int page, int pageSize, int myId, bool ownedByMe, bool ownedByOthers,
            bool assignedToOthers, bool completed);
        void SaveChanges();
    }

    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
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

        public ToDoItem FindById(int id)
        {
            return _db.ToDoItems.Find(id);
        }

        public int ToDoItemsCount()
        {
            return _db.ToDoItems.Count();
        }

        public int PendingToDoItemsCount(int userId)
        {
            return 0;
        }

        public IPagedList<ToDoItem> GetPagedToDoItems(int page, int pageSize, int myId, bool ownedByMe, bool ownedByOthers,
            bool assignedToOthers, bool completed)
        {
            var items = _db.ToDoItems as IEnumerable<ToDoItem>;

            if (!ownedByMe)
            {
                items = items.Where(x => x.OwnerId != myId);
            }
            if (!ownedByOthers)
            {
                items = items.Where(x => x.OwnerId == myId);
            }
            if (!assignedToOthers)
            {
                items = items.Where(x => x.AssigneeId == myId);
            }
            if (!completed)
            {
                items = items.Where(x => !x.Completed);
            }

            var sorted = items.OrderByDescending(x => x.TimeLeft.TotalSeconds).ToList();

            return sorted.ToPagedList(page, pageSize, sorted.Count);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}