using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CountDown.Controllers;
using Moq;
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
        public void Should_Redirect_User_To_The_Login_Action_If_He_Is_Not_Logged_In()
        {
            _sut.ControllerContext = GetMockControllerContext(false);
            var result = _sut.Index() as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("User"));
        }

        [Test]
        public void Should_Return_The_Index_View_If_The_User_Is_Logged_In()
        {
            _sut.ControllerContext = GetMockControllerContext(true);
            var result = _sut.Index() as ViewResult;
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }

        private ControllerContext GetMockControllerContext(bool authenticated)
        {
            // Lots of mocking is required to fake the ControllerContext.
            // The goal here is to force User.Identity.IsAuthenticated to return true or false.
            var mockPrincipal = new Mock<IPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.IsAuthenticated).Returns(authenticated);
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);

            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            return mockControllerContext.Object;
        }
    }
}
