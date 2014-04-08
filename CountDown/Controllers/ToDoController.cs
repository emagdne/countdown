using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Security;

namespace CountDown.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUserRepository _userRepository;

        public ToDoController()
        {
            _userRepository = new UserRepository();
            _toDoItemRepository = new ToDoItemRepository();
        }

        public ToDoController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _toDoItemRepository = new ToDoItemRepository();
        }

        public ToDoController(IUserRepository userRepository, IToDoItemRepository toDoItemRepository)
        {
            _userRepository = userRepository;
            _toDoItemRepository = toDoItemRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.assigneeid = GetAssigneeList();
                    return View("Create");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("SystemError");   
            }
        }

        [HttpPost]
        public ActionResult Create(ToDoItem item)
        {
            try
            {
                if (ModelState.IsValid && User.Identity.IsAuthenticated)
                {
                    _toDoItemRepository.InsertToDo(item);
                    _toDoItemRepository.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.assigneeid = GetAssigneeList();
                return View("Create");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        private IEnumerable<SelectListItem> GetAssigneeList()
        {
            var identity = User.Identity as CountDownIdentity;
            int currentUserId = identity != null 
                ? (User.Identity as CountDownIdentity).Id
                : 0;
            IEnumerable<SelectListItem> users = _userRepository.AllUsers()
                .Select(x => new SelectListItem
                {
                    Text = x.LastName + ", " + x.FirstName,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == currentUserId)
                })
                .OrderBy(x => x.Text);
            return users;
        }
    }
}
