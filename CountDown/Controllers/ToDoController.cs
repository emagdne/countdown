﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Security;

namespace CountDown.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
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

        public ToDoController(IToDoItemRepository toDoItemRepository)
        {
            _userRepository = new UserRepository();
            _toDoItemRepository = toDoItemRepository;
        }

        public ToDoController(IUserRepository userRepository, IToDoItemRepository toDoItemRepository)
        {
            _userRepository = userRepository;
            _toDoItemRepository = toDoItemRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (IsUserAuthenticated())
            {
                ViewBag.AssigneeList = GetAssigneeList();
                return View("Create");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(ToDoItem item)
        {
            if (IsUserAuthenticated() && ModelState.IsValid) 
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

        public ActionResult Edit(long? toDoItemId)
        {
            ActionResult response;

            if (IsUserAuthenticated() && toDoItemId.HasValue)
            {
                var id = toDoItemId.Value;
                var toDoItem = _toDoItemRepository.FindById(id);
                if (toDoItem != null && toDoItem.AssigneeId.HasValue)
                {
                    ViewBag.AssigneeList = GetAssigneeList(toDoItem.AssigneeId.Value);
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

        [HttpPost]
        public ActionResult Update(ToDoItem updatedItem)
        {
            ActionResult response;
            var originalItem = _toDoItemRepository.FindById(updatedItem.Id);

            if (IsUserAuthenticated() && originalItem != null)
            {
                if (AuthenticatedUser.Id == originalItem.OwnerId)
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
                    TempData["indexMessage"] = "You cannot update an item belonging to another user.";
                    response = RedirectToAction("Index", "Home");
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

        public ActionResult CancelEdit()
        {
            if (IsUserAuthenticated())
            {
                TempData["indexMessage"] = "No item was updated.";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(long? toDoItemId)
        {
            if (IsUserAuthenticated())
            {
                if (toDoItemId.HasValue)
                {
                    long id = toDoItemId.Value;
                    var toDoItem = _toDoItemRepository.FindById(id);

                    if (toDoItem != null)
                    {
                        if (!toDoItem.Completed)
                        {
                            if (AuthenticatedUser.Id == toDoItem.OwnerId)
                            {
                                _toDoItemRepository.DeleteToDo(toDoItem);
                                _toDoItemRepository.SaveChanges();
                                TempData["indexMessage"] = "Item deleted.";
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

        [HttpPost]
        public JsonResult Complete(long? toDoItemId)
        {
            JsonResult response;

            if (IsUserAuthenticated())
            {
                if (toDoItemId.HasValue)
                {
                    long id = toDoItemId.Value;
                    var todoItem = _toDoItemRepository.FindById(id);
                    if (todoItem != null)
                    {
                        if (AuthenticatedUser.Id == todoItem.AssigneeId)
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
                                TempData["indexMessage"] = "Item completed.";
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
                            response = JsonErrorResponse("The To-Do item you specified is not assigned to you.");
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

        private IEnumerable<SelectListItem> GetAssigneeList()
        {
            return GetAssigneeList(IsUserAuthenticated() ? AuthenticatedUser.Id : 0);
        }

        private IEnumerable<SelectListItem> GetAssigneeList(long selectedId)
        {
            return _userRepository.AllUsersByLastNameFirstName()
                .Select(x => new SelectListItem
                {
                    Text = x.LastName != null ? (x.LastName + ", " + x.FirstName) : x.FirstName,
                    Value = x.Id.ToString(CultureInfo.InvariantCulture),
                    Selected = (x.Id == selectedId)
                })
                .ToList();
        }
    }
}