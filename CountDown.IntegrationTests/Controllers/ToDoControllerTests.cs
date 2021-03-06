﻿using System;
using System.Security.Principal;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Security;
using Moq;
using Ninject;
using NUnit.Framework;

namespace CountDown.IntegrationTests.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    [TestFixture]
    public class A_ToDoController_Object
    {
        private ToDoController _sut;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IToDoItemRepository> _mockToDoItemRepository;
        private ToDoItem _toDoItem;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockToDoItemRepository = new Mock<IToDoItemRepository>();
            _sut = new ToDoController(_mockUserRepository.Object, _mockToDoItemRepository.Object);

            _toDoItem = ControllerTestsSetUp.Kernel.Get<ToDoItem>();
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Return_The_Create_View_When_The_User_Is_Logged_In_And_The_Create_Action_Is_Fired()
        {
            var countDownIdentity = new CountDownIdentity();
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Integration Tests: Feature 5")]
        public void Should_Save_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges());
        }

        [Test]
        [Category("Integration Tests: Feature 5")]
        public void Should_Redirect_To_The_Index_Action_After_Saving_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            var result = _sut.Create(_toDoItem) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 5")]
        public void Should_Not_Save_An_Invalid_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity();
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never());
        }

        [Test]
        [Category("Integration Tests: Feature 5")]
        public void Should_Stay_On_The_Create_Page_For_An_Invalid_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity();
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Create(_toDoItem) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Integration Tests: Feature 5")]
        [ExpectedException(typeof (Exception))]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_POST_Create_Action()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContextWithException();
            _sut.Create(_toDoItem);
        }

        [Test]
        [Category("Integration Tests: Feature 10")]
        public void Should_Return_The_Edit_View_When_The_User_Is_Logged_In_And_A_ToDo_Object_Is_Specified()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _toDoItem.AssigneeId = 0;

            var result = _sut.Edit(_toDoItem.Id) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        [Category("Integration Tests: Feature 10")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Edit_A_ToDo_Object()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(false);

            var result = _sut.Edit(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 10")]
        public void Should_Redirect_To_The_Index_Action_When_A_The_User_Attempts_To_Edit_A_Nonexistant_ToDo_Object()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Edit(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 10")]
        [ExpectedException(typeof (Exception))]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_By_The_Edit_Action()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            _sut.Edit(_toDoItem.Id);
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Mark_An_Existing_ToDo_Object_As_Completed_And_Save_Changes()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.AssigneeId = 1;
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges());
            Assert.That(_toDoItem.Completed, Is.True);
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Not_Mark_A_NonExistant_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Not_Mark_A_Completed_ToDo_Object_As_Completed_And_Save_Changes()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _toDoItem.AssigneeId = 1;
            _toDoItem.Completed = true;

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Return_Success_When_Marking_An_Existing_ToDo_Object_As_Completed()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _toDoItem.AssigneeId = 1;

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(IntegrationTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Success"));
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Return_Error_When_Marking_A_NonExistant_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(IntegrationTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(IntegrationTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified does not exist."));
        }

        [TestCase(1, 2)]
        [Category("Integration Tests: Feature 12")]
        public void
            Should_Return_Error_If_The_User_Attempts_To_Mark_A_ToDo_Object_As_Completed_That_Is_Not_Assigned_To_Him(
            long userId, long assigneeId)
        {
            var countDownIdentity = new CountDownIdentity {Id = userId};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.AssigneeId = assigneeId;
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(IntegrationTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(IntegrationTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified is not assigned to you."));
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Return_Error_When_Marking_A_Completed_ToDo_Object_As_Completed()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _toDoItem.AssigneeId = 1;
            _toDoItem.Completed = true;

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(IntegrationTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(IntegrationTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified has already been marked as completed."));
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        [ExpectedException(typeof (Exception))]
        public void Should_Return_Error_If_An_Unexpected_Exception_Is_Thrown_While_Marking_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            _sut.Complete(_toDoItem.Id);
        }

        [Test]
        [Category("Integration Tests: Feature 12")]
        public void Should_Return_Error_If_An_Unauthenticated_User_Attempts_To_Mark_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(false);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(IntegrationTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(IntegrationTestHelper.GetStandardJsonError(result),
                Is.EqualTo("You must be logged in to mark a To-Do item as completed."));
        }

        [Test]
        [Category("Integration Tests: Feature 13")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Delete_A_ToDo_Item()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(false);

            var result = _sut.Delete(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_Does_Not_Exist()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns((ToDoItem) null);

            _sut.Delete(_toDoItem.Id);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Delete item failed."));
        }

        [Test]
        [Category("Integration Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_Is_Completed()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _toDoItem.Completed = true;

            _sut.Delete(_toDoItem.Id);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("You cannot delete a completed item."));
        }

        [TestCase(1, 2)]
        [Category("Unit Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_He_Does_Not_Own(long userId,
            long ownerId)
        {
            var countDownIdentity = new CountDownIdentity {Id = userId};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = ownerId;
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            _sut.Delete(_toDoItem.Id);

            Assert.That(_sut.TempData["indexMessage"],
                Is.EqualTo("You cannot delete an item belonging to another user."));
        }

        [Test]
        [Category("Integration Tests: Feature 13")]
        [ExpectedException(typeof (Exception))]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_While_Deleting_A_ToDo_Item()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContextWithException();

            _sut.Delete(_toDoItem.Id);
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Update_A_ToDo_Object()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(false);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_If_The_Updated_ToDo_Object_Does_Not_Exist()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Return_Error_If_The_Updated_ToDo_Object_Does_Not_Exist()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            _sut.Update(_toDoItem);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Update item failed."));
        }

        [TestCase(1, 2)]
        [Category("Integration Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_If_The_User_Attempts_To_Update_A_ToDo_Item_That_He_Does_Not_Own(
            long userId, long ownerId)
        {
            var countDownIdentity = new CountDownIdentity {Id = userId};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = ownerId;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [TestCase(1, 2)]
        [Category("Integration Tests: Feature 14")]
        public void Should_Return_Error_If_The_User_Attempts_To_Update_A_ToDo_Item_That_He_Does_Not_Own(long userId,
            long ownerId)
        {
            var countDownIdentity = new CountDownIdentity {Id = userId};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = ownerId;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            _sut.Update(_toDoItem);

            Assert.That(_sut.TempData["indexMessage"],
                Is.EqualTo("You cannot update an item belonging to another user."));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Not_Update_An_Invalid_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);
            _toDoItem.OwnerId = 1;
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());

            _sut.Update(_toDoItem);

            _mockToDoItemRepository.Verify(x => x.UpdateToDo(It.IsAny<ToDoItem>(), It.IsAny<ToDoItem>()), Times.Never);
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Return_The_Edit_View_If_The_Updated_ToDo_Object_Is_Invalid()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = 1;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());

            var result = _sut.Update(_toDoItem) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Update_A_Valid_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = 1;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            _sut.Update(_toDoItem);

            _mockToDoItemRepository.Verify(x => x.SaveChanges());
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_After_Successfully_Updating_A_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = 1;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        public void Should_Return_A_Message_After_Successfully_Updating_A_ToDo_Object()
        {
            var countDownIdentity = new CountDownIdentity {Id = 1};
            _sut.ControllerContext =
                IntegrationTestHelper.GetMockControllerContextWithCountDownIdentity(countDownIdentity, true);
            _toDoItem.OwnerId = 1;
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            _sut.Update(_toDoItem);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Item updated."));
        }

        [Test]
        [Category("Integration Tests: Feature 14")]
        [ExpectedException(typeof (Exception))]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_While_Updating_A_ToDo_Object
            ()
        {
            _sut.ControllerContext = IntegrationTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            _sut.Update(_toDoItem);
        }
    }
}