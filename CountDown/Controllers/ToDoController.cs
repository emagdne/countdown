using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Security;

namespace CountDown.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class ToDoController : ApplicationController
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
                    ViewBag.AssigneeList = GetAssigneeList();
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
                    var identity = User.Identity as CountDownIdentity;
                    item.OwnerId = identity != null ? identity.Id : 0;

                    _toDoItemRepository.InsertToDo(item);
                    _toDoItemRepository.SaveChanges();
                    
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.AssigneeList = GetAssigneeList();
                return View("Create");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        public ActionResult Edit(int? toDoItemId)
        {
            try
            {
                ActionResult response;

                if (toDoItemId.HasValue && User.Identity.IsAuthenticated)
                {
                    var id = toDoItemId.Value;
                    var toDoItem = _toDoItemRepository.FindById(id);
                    if (toDoItem != null)
                    {
                        ViewBag.AssigneeList = GetAssigneeList((int)toDoItem.AssigneeId);
                        response = View("Edit", toDoItem);
                    }
                    else
                    {
                        response = RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    response = RedirectToAction("Index", "Home");
                }

                return response;
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        [HttpPost]
        public JsonResult Complete(int? toDoItemId)
        {
            try
            {
                JsonResult response;

                if (User.Identity.IsAuthenticated)
                {
                    if (toDoItemId.HasValue)
                    {
                        int id = toDoItemId.Value;
                        var todoItem = _toDoItemRepository.FindById(id);
                        if (todoItem != null)
                        {
                            if (!todoItem.Completed)
                            {
                                todoItem.Completed = true;

                                // Fill object with fake start/end dates/times to pass validation... it won't be persisted
                                todoItem.StartDate = DateTime.Now;
                                todoItem.StartTime = DateTime.Now;
                                todoItem.DueDate = DateTime.Now.AddDays(1);
                                todoItem.DueTime = DateTime.Now.AddDays(1);

                                _toDoItemRepository.SaveChanges();
                                response = JsonSuccessResponse();
                            }
                            else
                            {
                                response =
                                    JsonErrorResponse(
                                        "The To-Do item you specified has already been marked as completed.");
                            }
                        }
                        else
                        {
                            response = JsonErrorResponse("The To-Do item you specified does not exist.");
                        }
                    }
                    else
                    {
                        response = JsonErrorResponse("Missing argument: toDoItemId.");
                    }
                }
                else
                {
                    response = JsonErrorResponse("You must be logged in to mark a To-Do item as completed.");
                }

                return response;
            }
            catch (Exception)
            {
                return JsonErrorResponse();
            }
        }

        private IEnumerable<SelectListItem> GetAssigneeList()
        {
            var identity = User.Identity as CountDownIdentity;
            int currentUserId = identity != null
                ? (User.Identity as CountDownIdentity).Id
                : 0;

            return GetAssigneeList(currentUserId);
        }

        private IEnumerable<SelectListItem> GetAssigneeList(int selectedId)
        {
            IEnumerable<SelectListItem> users = _userRepository.AllUsers()
                .Select(x => new SelectListItem
                {
                    Text = x.LastName != null ? (x.LastName + ", " + x.FirstName) : x.FirstName,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == selectedId)
                })
                .OrderBy(x => x.Text);

            return users;
        }
    }
}