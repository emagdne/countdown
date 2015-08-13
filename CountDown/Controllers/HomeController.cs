using System.Web.Mvc;
using CountDown.Models.Repository;
using CountDown.Models.Security;

namespace CountDown.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class HomeController : ApplicationController
    {
        public const int PageSize = 10;
        private readonly IToDoItemRepository _toDoItemRepository;

        public HomeController()
        {
            _toDoItemRepository = new ToDoItemRepository();
        }

        public HomeController(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public ActionResult Index(int? page, bool? ownedByMe, bool? ownedByOthers, bool? assignedToOthers,
            bool? completed)
        {
            if (IsUserAuthenticated())
            {
                var identity = User.Identity as CountDownIdentity;
                var myId = (identity != null) ? identity.Id : 0;
                ViewData["UserId"] = myId;
                ViewData["TotalItemsPending"] = (identity != null)
                    ? _toDoItemRepository.PendingToDoItemsCount(identity.Id)
                    : 0;

                int pageIndex = page.HasValue ? page.Value - 1 : 0;
                var items = _toDoItemRepository.GetPagedToDoItems(pageIndex, PageSize, myId,
                    ownedByMe.HasValue ? ownedByMe.Value : true, ownedByOthers.HasValue ? ownedByOthers.Value : true,
                    assignedToOthers.HasValue ? assignedToOthers.Value : false,
                    completed.HasValue ? completed.Value : false);
                ViewData["TotalToDoItems"] = _toDoItemRepository.ToDoItemsCount();

                return View("Index", items);
            }
            TempData["loginMessage"] = "Please login or register to use the application.";
            return RedirectToAction("Login", "User");
        }
    }
}