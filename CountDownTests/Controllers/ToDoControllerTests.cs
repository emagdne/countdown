using System.Web.Mvc;
using CountDown.Controllers;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class A_ToDoController_Object
    {
        private ToDoController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ToDoController();
        }

        [Test]
        public void Should_Return_The_Create_View_When_The_Create_Action_Is_Fired()
        {
            var result = _sut.Create() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }
    }
}
