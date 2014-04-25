using System;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using Moq;
using NUnit.Framework;

namespace CountDown.UnitTests.Controllers
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    [TestFixture]
    public class A_ToDoController_Object
    {
        private ToDoController _sut;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IToDoItemRepository> _mockToDoItemRepository;
        private Mock<ToDoItem> _mockToDoItem;
        private ToDoItem _toDoItem;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockToDoItemRepository = new Mock<IToDoItemRepository>();
            _sut = new ToDoController(_mockUserRepository.Object, _mockToDoItemRepository.Object);

            _mockToDoItem = new Mock<ToDoItem>();
            _toDoItem = _mockToDoItem.Object;
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Return_The_Create_View_When_The_User_Is_Logged_In_And_The_Create_Action_Is_Fired()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Redirect_To_The_Index_Action_When_The_User_Is_Not_Logged_In_And_The_Create_Action_Is_Fired()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);
            var result = _sut.Create() as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Save_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges());
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Redirect_To_The_Index_Action_After_Saving_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Create(_toDoItem) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Not_Save_An_Invalid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            _sut.Create(_toDoItem);
            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never());
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Stay_On_The_Create_Page_For_An_Invalid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Create(_toDoItem) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_GET_Create_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 5")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_POST_Create_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();
            var result = _sut.Create(_toDoItem) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 10")]
        public void Should_Return_The_Edit_View_When_The_User_Is_Logged_In_And_A_ToDo_Object_Is_Specified()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _mockToDoItem.Setup(x => x.AssigneeId).Returns(0);

            var result = _sut.Edit(_toDoItem.Id) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        [Category("Unit Tests: Feature 10")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Edit_A_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.Edit(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 10")]
        public void Should_Redirect_To_The_Index_Action_When_A_ToDo_Object_Id_Is_Not_Given_And_The_Edit_Action_Is_Fired()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            var result = _sut.Edit(null) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 10")]
        public void Should_Redirect_To_The_Index_Action_When_A_The_User_Attempts_To_Edit_A_Nonexistant_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Edit(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 10")]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_By_The_Edit_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            var result = _sut.Edit(_toDoItem.Id) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Mark_An_Existing_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _mockToDoItem.SetupSet(x => x.Completed = true).Verifiable();

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges());
            _mockToDoItem.VerifySet(x => x.Completed = true);
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Not_Mark_A_NonExistant_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Not_Mark_A_Completed_ToDo_Object_As_Completed_And_Save_Changes()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _mockToDoItem.Setup(x => x.Completed).Returns(true);

            _sut.Complete(_toDoItem.Id);

            _mockToDoItemRepository.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Return_Success_When_Marking_An_Existing_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Success"));
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
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
        [Category("Unit Tests: Feature 12")]
        public void Should_Return_Error_When_Marking_A_Completed_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _mockToDoItem.Setup(x => x.Completed).Returns(true);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("The To-Do item you specified has already been marked as completed."));
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Return_Error_If_A_ToDo_Object_Id_Is_Not_Given_While_Marking_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            var result = _sut.Complete(null);
            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));

            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("Missing argument: toDoItemId."));
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Return_Error_If_An_Unexpected_Exception_Is_Thrown_While_Marking_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("An unexpected error occurred while processing your request."));
        }

        [Test]
        [Category("Unit Tests: Feature 12")]
        public void Should_Return_Error_If_An_Unauthenticated_User_Attempts_To_Mark_A_ToDo_Object_As_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.Complete(_toDoItem.Id);

            Assert.That(UnitTestHelper.GetStandardJsonStatus(result), Is.EqualTo("Error"));
            Assert.That(UnitTestHelper.GetStandardJsonError(result),
                Is.EqualTo("You must be logged in to mark a To-Do item as completed."));
        }

        [Test]
        [Category("Unit Tests: Feature 13")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Delete_A_ToDo_Item()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.Delete(_toDoItem.Id) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_Without_Providing_An_Id()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            _sut.Delete(null);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("You must specify a To-Do item to delete."));
        }

        [Test]
        [Category("Unit Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_Does_Not_Exist()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns((ToDoItem) null);

            _sut.Delete(_toDoItem.Id);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Delete item failed."));
        }

        [Test]
        [Category("Unit Tests: Feature 13")]
        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_Is_Completed()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(_toDoItem.Id)).Returns(_toDoItem);
            _mockToDoItem.Setup(x => x.Completed).Returns(true);

            _sut.Delete(_toDoItem.Id);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("You cannot delete a completed item."));
        }

//        [TestCase(1, 2)]
//        [Category("Unit Tests: Feature 13")]
//        [Ignore("Can only be verified in integration testing due to dependency on CountDownIdentity.")]
//        public void Should_Return_Error_If_The_User_Attempts_To_Delete_A_ToDo_Item_That_He_Does_Not_Own(int userId,
//            int ownerId)
//        {
//        }
//
//        [Test]
//        [Category("Unit Tests: Feature 13")]
//        [Ignore("Can only be verified in integration testing due to dependency on CountDownIdentity.")]
//        public void Should_Delete_A_Valid_ToDo_Item()
//        {
//        }
//
//        [Test]
//        [Category("Unit Tests: Feature 13")]
//        [Ignore("Can only be verified in integration testing due to dependency on CountDownIdentity.")]
//        public void Should_A_Message_After_Deleting_A_Valid_ToDo_Item()
//        {
//        }
//
//        [Test]
//        [Category("Unit Tests: Feature 13")]
//        [Ignore("Can only be verified in integration testing due to dependency on CountDownIdentity.")]
//        public void Should_Redirect_To_The_Index_Action_After_Deleting_A_ToDo_Item()
//        {
//        }

        [Test]
        [Category("Unit Tests: Feature 13")]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_While_Deleting_A_ToDo_Item()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();

            var result = _sut.Delete(_toDoItem.Id) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Attempts_To_Update_A_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_If_The_Updated_ToDo_Object_Does_Not_Exist()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_An_Error_Message_If_The_Updated_ToDo_Object_Does_Not_Exist()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((ToDoItem) null);

            _sut.Update(_toDoItem);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Update item failed."));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Not_Update_An_Invalid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());

            _sut.Update(_toDoItem);

            _mockToDoItemRepository.Verify(x => x.UpdateToDo(It.IsAny<ToDoItem>(), It.IsAny<ToDoItem>()), Times.Never);
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_The_Edit_View_If_The_Updated_ToDo_Object_Is_Invalid()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());

            var result = _sut.Update(_toDoItem) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Update_A_Valid_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            _sut.Update(_toDoItem);

            _mockToDoItemRepository.Verify(x => x.SaveChanges());
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_After_Successfully_Updating_A_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            var result = _sut.Update(_toDoItem) as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_A_Message_After_Successfully_Updating_A_ToDo_Object()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Returns(_toDoItem);

            _sut.Update(_toDoItem);

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("Item updated."));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_While_Updating_A_ToDo_Object
            ()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.FindById(It.IsAny<long>())).Throws(new Exception());

            var result = _sut.Update(_toDoItem) as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_When_An_Unauthenticated_User_Cancels_An_Edit()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);

            var result = _sut.CancelEdit() as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Redirect_To_The_Index_Action_When_The_User_Cancels_An_Edit()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            var result = _sut.CancelEdit() as RedirectToRouteResult;

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_A_Message_When_The_User_Cancels_An_Edit()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);

            _sut.CancelEdit();

            Assert.That(_sut.TempData["indexMessage"], Is.EqualTo("No item was updated."));
        }

        [Test]
        [Category("Unit Tests: Feature 14")]
        public void Should_Return_The_SystemError_View_If_An_Unexpected_Exception_Is_Thrown_While_Canceling_An_Edit()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();

            var result = _sut.CancelEdit() as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }
    }
}