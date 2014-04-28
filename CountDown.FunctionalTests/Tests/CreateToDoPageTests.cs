using System;
using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests.Tests
{
    [TestFixture]
    public class The_CreateToDo_Page
    {
        private const string DateFormat = "yyyy/MM/dd";
        private const string TimeFormat = "h:mm tt";

        [TestFixtureSetUp]
        public void SetUpOnce()
        {
            FunctionalTestHelper.SignInAsTestUser();
        }

        [SetUp]
        public void SetUp()
        {
            CountDownApp.GoToCreateToDoPage();
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void By_Default_Should_Have_The_Current_Date_As_The_Start_Date()
        {
            Assert.That(CountDownApp.CreateToDoPage.StartDateField.Value,
                Is.EqualTo(String.Format("{0:" + DateFormat + "}", DateTime.Now)));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void By_Default_Should_Have_The_Current_Time_As_The_Start_Time()
        {
            Assert.That(CountDownApp.CreateToDoPage.StartTimeField.Value,
                Is.EqualTo(String.Format("{0:" + TimeFormat + "}", DateTime.Now)));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void By_Default_Should_Have_Self_Selected_In_The_Assign_To_Dropdown()
        {
            Assert.That(CountDownApp.CreateToDoPage.AssignToDropdown.Value, Is.EqualTo("Self"));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Have_Self_Listed_First_In_The_Assign_To_Dropdown_Followed_By_Users_Sorted_Alphabetically()
        {
            var options = CountDownApp.CreateToDoPage.AssignToDropdown.Options;
            Assert.That(options[0], Is.EqualTo("Self"));
            for (int i = 2; i < options.Count; i++)
            {
                Assert.That(String.Compare(options[i], options[i - 1], StringComparison.Ordinal),
                    Is.GreaterThanOrEqualTo(0));
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Missing_Title()
        {
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.TitleField.ErrorMessage, Is.EqualTo("You must provide a title."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Title_Greater_Than_50_Characters()
        {
            CountDownApp.CreateToDoPage.TitleField.Fill(FunctionalTestHelper.RandomString(51));
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.TitleField.ErrorMessage,
                Is.EqualTo("The title must be from 1 to 50 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Description_Greater_Than_200_Characters()
        {
            CountDownApp.CreateToDoPage.DescriptionField.Fill(FunctionalTestHelper.RandomString(201));
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.DescriptionField.ErrorMessage,
                Is.EqualTo("The description must be from 0 to 200 characters in length."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Missing_Start_Date()
        {
            CountDownApp.CreateToDoPage.StartDateField.Clear();
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.StartDateField.ErrorMessage,
                Is.EqualTo("You must provide a start date."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Missing_Start_Time()
        {
            CountDownApp.CreateToDoPage.StartTimeField.Clear();
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.StartTimeField.ErrorMessage,
                Is.EqualTo("You must provide a start time."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Missing_Due_Date()
        {
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.DueDateField.ErrorMessage,
                Is.EqualTo("You must provide a due date."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_For_Missing_Due_Time()
        {
            CountDownApp.CreateToDoPage.ClickSubmit();
            Assert.That(CountDownApp.CreateToDoPage.DueTimeField.ErrorMessage,
                Is.EqualTo("You must provide a due time."));
        }

        [Test]
        [TestCase("2014/01/01", "12:00 PM", "2014/01/01", "12:00 PM")]
        [TestCase("2014/01/01", "12:00 PM", "2014/01/01", "11:59 AM")]
        [TestCase("2014/01/01", "12:00 AM", "2013/12/31", "11:59 PM")]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Report_Error_If_The_Due_Date_And_Time_Occurs_On_Or_Before_The_Start_Date_And_Time(
            string startDate, string startTime, string dueDate, string dueTime)
        {
            CountDownApp.CreateToDoPage.TitleField.Fill(FunctionalTestHelper.RandomString());
            CountDownApp.CreateToDoPage.StartDateField.Clear();
            CountDownApp.CreateToDoPage.StartTimeField.Clear();

            CountDownApp.CreateToDoPage.StartDateField.Fill(startDate);
            CountDownApp.CreateToDoPage.StartTimeField.Fill(startTime);
            CountDownApp.CreateToDoPage.DueDateField.Fill(dueDate);
            CountDownApp.CreateToDoPage.DueTimeField.Fill(dueTime);
            CountDownApp.CreateToDoPage.ClickSubmit();

            Assert.That(CountDownApp.CreateToDoPage.DueDateField.ErrorMessage,
                Is.EqualTo("The due date and time must be after the start date and time."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Return_To_The_Home_Page_When_The_Cancel_Button_Is_Clicked()
        {
            CountDownApp.CreateToDoPage.ClickCancel();
            Assert.That(CountDownApp.IsOnHomePage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Return_To_The_Home_Page_When_The_Form_Is_Valid_And_The_Submit_Button_Is_Clicked()
        {
            const string title = "Test ToDo Item Generated By Test Case - Delete Me!";
            const string description = "Generated by " +
                                       "Should_Return_To_The_Home_Page_When_The_Form_Is_Valid_And_The_Submit_Button_Is_Clicked() " +
                                       "in CreateToDoPageTests.cs";

            CountDownApp.CreateToDoPage.TitleField.Fill(title);
            CountDownApp.CreateToDoPage.DescriptionField.Fill(description);
            CountDownApp.CreateToDoPage.StartDateField.Clear();
            CountDownApp.CreateToDoPage.StartTimeField.Clear();

            CountDownApp.CreateToDoPage.StartDateField.Fill("2014/01/01");
            CountDownApp.CreateToDoPage.StartTimeField.Fill("12:00 PM");
            CountDownApp.CreateToDoPage.DueDateField.Fill("2014/03/02");
            CountDownApp.CreateToDoPage.DueTimeField.Fill("12:00 PM");
            CountDownApp.CreateToDoPage.ClickSubmit();
            CountDownDatabase.DeleteToDoItem(title);

            Assert.That(CountDownApp.IsOnHomePage, Is.True);
        }
    }
}