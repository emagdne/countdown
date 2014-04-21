using System.Collections.Generic;
using System.Data.Entity;
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
        void UpdateToDo(ToDoItem originalItem, ToDoItem updatedItem);
        void DeleteToDo(ToDoItem item);
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

        /// <summary>
        /// <para>Precondition: The original item has a value for Id and exists in the database.</para>
        /// <para>
        ///     Postcondition: The properties Title, Description, StartDate, StartTime,
        ///     DueDate, DueTime, and AssigneeId in the original item are reassigned to the
        ///     values within the updated item.
        /// </para>
        /// </summary>
        public void UpdateToDo(ToDoItem originalItem, ToDoItem updatedItem)
        {
            originalItem.Title = updatedItem.Title;
            originalItem.Description = updatedItem.Description;
            originalItem.StartDate = updatedItem.StartDate;
            originalItem.StartTime = updatedItem.StartTime;
            originalItem.DueDate = updatedItem.DueDate;
            originalItem.DueTime = updatedItem.DueTime;
            originalItem.AssigneeId = updatedItem.AssigneeId;

            // Start and Due should only be updated if their dependencies are available.
            if (updatedItem.StartDate.HasValue && updatedItem.StartTime.HasValue)
            {
                originalItem.Start = updatedItem.StartDate.Value.Date + updatedItem.StartDate.Value.TimeOfDay;
            }
            if (updatedItem.DueDate.HasValue && updatedItem.DueTime.HasValue)
            {
                originalItem.Due = updatedItem.DueDate.Value.Date + updatedItem.DueTime.Value.TimeOfDay;
            }
        }

        public void DeleteToDo(ToDoItem item)
        {
            _db.ToDoItems.Remove(item);
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

            // Every to-do item should have a value for TimeLeft. If for some unknown reason an item is missing TimeLeft,
            // sort it by Id.
            var sorted = items
                .OrderByDescending(x => x.TimeLeft.HasValue ? x.TimeLeft.Value.TotalSeconds : x.Id)
                .ToList();

            return sorted.ToPagedList(page, pageSize, sorted.Count);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}