using CountDown.Models.Domain;
using NUnit.Framework;

namespace CountDown.UnitTests.Models
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    [TestFixture]
    public class A_LoginAttempt_Object
    {
        private LoginAttempt _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LoginAttempt();
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Require_An_Email_Address()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("Email address is required."));
        }

        [Test]
        [Category("Unit Tests: Feature 4")]
        public void Should_Require_A_Password()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("Password is required."));
        }
    }
}