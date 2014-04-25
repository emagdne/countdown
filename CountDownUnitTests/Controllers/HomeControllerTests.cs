using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using Moq;
using MvcPaging;
using NUnit.Framework;

namespace CountDown.UnitTests.Controllers
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    [TestFixture]
    public class A_HomeController_Object
    {
        private HomeController _sut;
        private Mock<IToDoItemRepository> _mockToDoItemRepository;

        [SetUp]
        public void SetUp()
        {
            _mockToDoItemRepository = new Mock<IToDoItemRepository>();
            _sut = new HomeController(_mockToDoItemRepository.Object);
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Redirect_User_To_The_Login_Action_If_He_Is_Not_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);
            var result = _sut.Index(null, null, null, null, null) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("User"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Return_The_Index_View_If_User_Is_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Index(null, null, null, null, null) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Unit Tests: Feature 6")]
        public void Should_Return_A_List_Of_ToDo_Items_That_Does_Not_Exceed_The_Max_Per_Page()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _sut.Index(null, null, null, null, null);

            // Verify that the function is called with the correct parameters
            _mockToDoItemRepository.Verify(x =>
                x.GetPagedToDoItems(It.IsAny<int>(), HomeController.PageSize, It.IsAny<long>(), It.IsAny<bool>(),
                    It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit Tests: Feature 6")]
        public void Should_Return_The_Correct_List_Of_ToDo_Items_For_The_Current_Page(int page)
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var mockList = new Mock<IPagedList<ToDoItem>>().Object;
            _mockToDoItemRepository.Setup(x =>
                x.GetPagedToDoItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<bool>(),
                    It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(mockList);

            var result = _sut.Index(page, null, null, null, null) as ViewResult;
            var resultList = result.Model as IPagedList;

            // Verify that the function is called with the correct parameters
            _mockToDoItemRepository.Verify(x =>
                x.GetPagedToDoItems(page - 1, HomeController.PageSize, It.IsAny<long>(), It.IsAny<bool>(),
                    It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()));

            // Assert that the list is correctly given to the view
            Assert.That(resultList, Is.EqualTo(mockList));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(8)]
        [Category("Unit Tests: Feature 6")]
        public void Should_Return_The_Correct_Number_Of_Total_ToDoItems(int numItems)
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            _mockToDoItemRepository.Setup(x => x.ToDoItemsCount()).Returns(numItems);

            var result = _sut.Index(null, null, null, null, null) as ViewResult;

            Assert.That(result.ViewData["TotalToDoItems"], Is.EqualTo(numItems));
        }
    }
}