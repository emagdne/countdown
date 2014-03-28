using System.Web.Mvc;
using CountDown.Controllers;
using NUnit.Framework;

namespace CountDownTests.Controllers
{
    [TestFixture]
    public class ToDoControllerTests
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

        [Test]
        public void Should_Return_The_List_View_When_The_List_Action_Is_Fired()
        {
            var result = _sut.List() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("List"));
        }
    }
}
