using CountDown.Models.Domain;
using NUnit.Framework;

namespace CountDownUnitTests.Models
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    [TestFixture]
    public class A_User_Object
    {
        private User _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new User();
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Require_A_FirstName()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a first name."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_FirstName_Greater_Than_50_Characters()
        {
            _sut.FirstName = new string('x',51);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The first name must be from 1 to 50 characters in length."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_LastName_Greater_Than_50_Characters()
        {
            _sut.LastName = new string('x',51);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The last name must be from 0 to 50 characters in length."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Require_An_Email()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide an email address."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_An_Invalid_Email()
        {
            _sut.Email = "InvalidEmail";
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("Please enter a valid email address."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Require_An_Email_Confirmation()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must confirm your email address."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_An_Email_Confirmation_Not_Equal_To_The_Email()
        {
            _sut.Email = "tester@gmail.com";
            _sut.ConfirmEmail = "DoesNotMatch";
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("Emails do not match."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Require_A_Password()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a password."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_Password_Less_Than_4_Characters()
        {
            _sut.Password = new string('x',3);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_Password_Greater_Than_50_Characters()
        {
            _sut.Password = new string('x', 51);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The password must be from 4 to 50 characters in length."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Require_A_Password_Confirmation()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must confirm your password."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_Password_Confirmation_Not_Equal_To_The_Password()
        {
            _sut.Password = "TestPassword";
            _sut.ConfirmPassword = "DoesNotMatch";
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("Passwords do not match."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_Hash_Greater_Than_68_Characters()
        {
            _sut.Hash = new string('x',67);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The hash must be 68 characters long."));
        }

        [Test]
        [Category("Unit Tests: Feature 1")]
        public void Should_Reject_A_Hash_Less_Than_68_Characters()
        {
            _sut.Hash = new string('x', 69);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The hash must be 68 characters long."));
        }
    }
}
