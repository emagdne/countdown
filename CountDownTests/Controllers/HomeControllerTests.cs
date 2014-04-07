using System.Web.Mvc;
using CountDown.Controllers;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class A_HomeController_Object
    {
        private HomeController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HomeController();    
        }

        [Test]
        [Category("Iteration 2")]
        public void Should_Redirect_User_To_The_Login_Action_If_He_Is_Not_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(false);
            var result = _sut.Index() as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("User"));
        }

        [Test]
        [Category("Iteration 2")]
        public void Should_Return_The_Index_View_If_User_Is_Logged_In()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContext(true);
            var result = _sut.Index() as ViewResult;
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }
    }
}
