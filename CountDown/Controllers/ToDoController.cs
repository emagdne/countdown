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

                    TempData["indexMessage"] = "Item created.";
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

        public ActionResult Edit(long? toDoItemId)
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
                        ViewBag.AssigneeList = GetAssigneeList((int) toDoItem.AssigneeId);
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
        public ActionResult Update(ToDoItem updatedItem)
        {
            try
            {
                ActionResult response;
                var originalItem = _toDoItemRepository.FindById(updatedItem.Id);

                if (User.Identity.IsAuthenticated && originalItem != null)
                {
                    if (ModelState.IsValid)
                    {
                        _toDoItemRepository.UpdateToDo(originalItem, updatedItem);
                        _toDoItemRepository.SaveChanges();
                        TempData["indexMessage"] = "Item updated.";
                        response = RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Manually give the updated item a reference to its owner -- it won't
                        // have it since the object was not pulled from the database
                        updatedItem.Owner = originalItem.Owner;

                        ViewBag.AssigneeList =
                            GetAssigneeList(updatedItem.AssigneeId.HasValue ? updatedItem.AssigneeId.Value : 0);

                        // Open editing to allow user to correct changes
                        ViewData["OpenEditing"] = true;
                        response = View("Edit", updatedItem);
                    }
                }
                else
                {
                    if (originalItem == null)
                    {
                        TempData["indexMessage"] = "Update item failed.";
                    }
                    response = RedirectToAction("Index", "Home");
                }

                return response;
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        public ActionResult CancelEdit()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    TempData["indexMessage"] = "No item was updated.";                    
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        [HttpPost]
        public ActionResult Delete(long? toDoItemId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (toDoItemId.HasValue)
                    {
                        long id = toDoItemId.Value;
                        var toDoItem = _toDoItemRepository.FindById(id);

                        if (toDoItem != null)
                        {

                            if (!toDoItem.Completed)
                            {
                                var identity = User.Identity as CountDownIdentity;

                                if (identity.Id == toDoItem.OwnerId)
                                {
                                    TempData["indexMessage"] = "Item deleted.";
                                    _toDoItemRepository.DeleteToDo(toDoItem);
                                    _toDoItemRepository.SaveChanges();
                                }
                                else
                                {
                                    TempData["indexMessage"] = "You cannot delete an item belonging to another user.";
                                }
                            }
                            else
                            {
                                TempData["indexMessage"] = "You cannot delete a completed item.";
                            }
                        }
                        else
                        {
                            TempData["indexMessage"] = "Delete item failed.";
                        }
                    }
                    else
                    {
                        TempData["indexMessage"] = "You must specify a To-Do item to delete.";
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        [HttpPost]
        public JsonResult Complete(long? toDoItemId)
        {
            try
            {
                JsonResult response;

                if (User.Identity.IsAuthenticated)
                {
                    if (toDoItemId.HasValue)
                    {
                        long id = toDoItemId.Value;
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
            long currentUserId = identity != null
                ? (User.Identity as CountDownIdentity).Id
                : 0;

            return GetAssigneeList(currentUserId);
        }

        private IEnumerable<SelectListItem> GetAssigneeList(long selectedId)
        {
            IEnumerable<SelectListItem> users = _userRepository.AllUsers().ToList()
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