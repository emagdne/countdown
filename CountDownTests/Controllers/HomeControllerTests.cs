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
        public void Should_Return_The_Index_View_When_Index_Action_Is_Fired()
        {
            var result = _sut.Index() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }
    }
}
