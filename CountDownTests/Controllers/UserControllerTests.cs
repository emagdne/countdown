using System.Web.Mvc;
using CountDown.Controllers;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class A_UserController_Object
    {
        private UserController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserController();
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
    }
}
