using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Repository;
using Moq;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class A_HomeController_Object
    {
        private HomeController _sut;
        private Mock<IToDoItemRepository> _toDoItemRepository;

        [SetUp]
        public void SetUp()
        {
            _toDoItemRepository = new Mock<IToDoItemRepository>();
            _sut = new HomeController(_toDoItemRepository.Object);
        }

        [Test]
        [Category("Iteration 4")]
        public void Should_Redirect_User_To_The_Login_Action_If_He_Is_Not_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);
            var result = _sut.Index(null, null, null, null, null) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("User"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        [Category("Iteration 4")]
        public void Should_Return_The_Index_View_If_User_Is_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Index(null, null, null, null, null) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }
    }
}