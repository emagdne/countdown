using System;
using System.Web.Mvc;
using CountDown.Controllers;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using Moq;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class A_UserController_Object
    {
        private Mock<IUserRepository> _mockRepository;
        private UserController _sut;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IUserRepository>();
            _sut = new UserController(_mockRepository.Object);
            _user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Email = "user@yahoo.com",
                ConfirmEmail = "user@yahoo.com",
                Password = "12345",
                ConfirmPassword = "12345",
                Hash = "ABC123",
                Salt = "123ABC"
            };
        }

        [Test]
        public void Should_Return_The_Registration_Page_When_The_Register_Action_Is_Fired()
        {
            var result = _sut.Register() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Registration"));
        }

        [Test]
        public void Should_Return_The_Login_Page_When_The_Login_Action_Is_Fired()
        {
            var result = _sut.Login() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        public void Should_Save_A_Valid_User_Object()
        {
            _sut.Register(_user);
            _mockRepository.Verify(r => r.InsertUser(_user));
        }

        [Test]
        public void Should_Return_To_Login_Page_After_Saving_A_Valid_Registration_Object()
        {
            var result = _sut.Register(_user) as RedirectToRouteResult;
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void Should_Not_Save_An_Invalid_Registration_Object()
        {
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            _sut.Register(_user);
            _mockRepository.Verify(r => r.InsertUser(It.IsAny<User>()), Times.Never());
        }

        [Test]
        public void Should_Stay_On_The_Registration_Page_For_An_Invalid_Registration_Object()
        {
            _sut.ModelState.AddModelError(String.Empty, It.IsAny<String>());
            var result = _sut.Register(_user) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Registration"));
        }

        [Test]
        public void Should_Return_The_SystemErrorPage_If_An_Unexpected_Exception_Is_Thrown()
        {
            _mockRepository.Setup(r => r.InsertUser(It.IsAny<User>())).Throws<Exception>();
            var result = _sut.Register(_user) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("SystemError"));
        }
    }
}
