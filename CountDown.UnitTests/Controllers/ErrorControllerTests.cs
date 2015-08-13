using System.Web.Mvc;
using CountDown.Controllers;
using NUnit.Framework;

namespace CountDown.UnitTests.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    [TestFixture]
    public class An_ErrorController_Object
    {
        private ErrorController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ErrorController();
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Return_The_SystemError_Page_When_The_Index_Action_Is_Fired()
        {
            var result = _sut.Index() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }
    }
}
