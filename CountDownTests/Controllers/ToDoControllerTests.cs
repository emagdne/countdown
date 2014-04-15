﻿using System;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using Moq;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
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
            _toDoItem = new ToDoItem
            {
                Title = "Test Title",
                StartDate = DateTime.Now,
                StartTime = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                DueTime = DateTime.Now.AddDays(1),
                AssigneeId = 1
            };
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Return_The_Create_View_When_The_User_Is_Logged_In_And_The_Create_Action_Is_Fired()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Redirect_To_The_Index_Action_When_The_User_Is_Not_Logged_In_And_The_Create_Action_Is_Fired()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);
            var result = _sut.Create() as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Save_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges());
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Redirect_To_The_Index_Action_After_Saving_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Create(_toDoItem) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Not_Save_An_Invalid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never());
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Stay_On_The_Create_Page_For_An_Invalid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Create(_toDoItem) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_GET_Create_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Feature 5")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_POST_Create_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();
            var result = _sut.Create(_toDoItem) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Mark_An_Existing_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges());
            Assert.That(_toDoItem.Completed, Is.True);
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Not_Mark_A_NonExistant_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Not_Mark_A_Completed_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _toDoItem.Completed = true;
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Success_When_Marking_An_Existing_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Success"));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Error_When_Marking_A_NonExistant_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified does not exist."));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Error_When_Marking_A_Completed_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _toDoItem.Completed = true;
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified has already been marked as completed."));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Error_If_A_ToDo_Object_Id_Is_Not_Given_While_Marking_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            var result = _sut.Complete(null);
            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));

            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("Missing argument: toDoItemId."));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Error_If_An_Unexpected_Exception_Is_Thrown_While_Marking_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Throws(new Exception());

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("An unexpected error occurred while processing your request."));
        }

        [Test]
        [Category("Feature 12")]
        public void Should_Return_Error_If_An_Unauthenticated_User_Attempts_To_Mark_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("You must be logged in to mark a To-Do item as completed."));
        }
    }
}
