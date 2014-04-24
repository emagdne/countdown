using System;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Service;
using Moq;
using NUnit.Framework;

namespace CountDownUnitTests.Controllers
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    [TestFixture]
    public class A_UserController_Object
    {
        private Mock<IUserRepository> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private Mock<User> _mockUser;
        private Mock<LoginAttempt> _mockLoginAttempt;
        private UserController _sut;
        private User _user;
        private LoginAttempt _loginAttempt;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IUserRepository>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _sut = new UserController(_mockRepository.Object, _mockAuthenticationService.Object);

            _mockUser = new Mock<User>();
            _mockLoginAttempt = new Mock<LoginAttempt>();
            _user = _mockUser.Object;
            _loginAttempt = _mockLoginAttempt.Object;
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Return_The_Registration_Page_When_The_Register_Action_Is_Fired()
        {
            var result = _sut.Register() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Registration"));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Save_A_Valid_User_Object()
        {
            _sut.Register(_user);
            _mockRepository.Verify(r => r.InsertUser(_user));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Return_To_Login_Page_After_Saving_A_Valid_Registration_Object()
        {
            var result = _sut.Register(_user) as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Not_Save_An_Invalid_Registration_Object()
        {
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            _sut.Register(_user);
            _mockRepository.Verify(r => r.InsertUser(It.IsAny<User>()), Times.Never());
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Stay_On_The_Registration_Page_For_An_Invalid_Registration_Object()
        {
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Register(_user) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Registration"));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_Register_Action()
        {
            _mockRepository.Setup(r => r.InsertUser(It.IsAny<User>())).Throws<Exception>();
            var result = _sut.Register(_user) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Return_The_Login_Page_When_The_Login_Action_Is_Fired()
        {
            var result = _sut.Login() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Redirect_User_To_The_Index_Action_If_Authentication_Is_Successful()
        {
            _mockAuthenticationService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _sut.Login(_loginAttempt);
            _mockAuthenticationService.Verify(x => x.HandleLoginRedirect(It.IsAny<string>(), It.IsAny<bool>()));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Return_The_Login_View_If_Authentication_Is_Not_Successful()
        {
            _mockAuthenticationService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var result = _sut.Login(_loginAttempt) as ViewResult;
            _mockAuthenticationService.Verify(x => x.HandleLoginRedirect(It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Stay_On_The_Login_Page_For_An_Invalid_LoginAttempt_Object()
        {
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Login(_loginAttempt) as ViewResult;
            _mockAuthenticationService.Verify(x => x.HandleLoginRedirect(It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_GET_Login_Action()
        {
            _sut.ControllerContext = UnitTestHelper.GetMockControllerContextWithException();
            var result = _sut.Login() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Return_The_SystemError_Page_If_An_Unexpected_Exception_Is_Thrown_By_The_POST_Login_Action()
        {
            _mockAuthenticationService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var result = _sut.Login(_loginAttempt) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }
    }
}