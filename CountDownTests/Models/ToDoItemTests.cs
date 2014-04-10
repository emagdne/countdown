using System;
using CountDown.Models.Domain;
using NUnit.Framework;

namespace CountDownTests.Models
{
    [TestFixture]
    public class A_ToDoItem_Object
    {
        private ToDoItem _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ToDoItem();
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_A_Title()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a title."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Reject_A_Title_Greater_Than_50_Characters()
        {
            _sut.Title = new string('x', 51);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The title must be from 1 to 50 characters in length."));                        
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Reject_A_Description_Greater_Than_500_Characters()
        {
            _sut.Description = new string('x', 501);
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The description must be from 0 to 500 characters in length."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_A_StartDate()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a start date."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_A_StartTime()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a start time."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_A_DueDate()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a due date."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_A_DueTime()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must provide a due time."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Not_Allow_A_StartDate_And_Time_To_Occur_After_A_DueDate_And_Time()
        {
            // These attributes must be assigned... all attributes must pass validation before the
            // Validate(..) method will execute.
            _sut.Title = "Test Title";
            _sut.AssigneeId = 0;

            // Set start date one day after the due date
            _sut.StartDate = DateTime.Now.AddDays(1);
            _sut.StartTime = DateTime.Now;
            _sut.DueDate = DateTime.Now;
            _sut.DueTime = DateTime.Now;

            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("The due date and time must be after the start date and time."));
        }

        [Test]
        [Category("Iteration 5")]
        public void Should_Require_An_AssigneeId()
        {
            var errors = UnitTestHelper.GetValidationErrors(_sut);
            Assert.That(errors, Has.Member("You must assign the to-do item to someone."));
        }
    }
}
