using System;
using CountDown.FunctionalTests.Data;
using CountDown.FunctionalTests.Data.TestData;
using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests.Tests
{
    [TestFixture]
    public class The_EditToDo_Page
    {
        [TestFixtureSetUp]
        public void SetUpOnce()
        {
            FunctionalTestHelper.SignInAsTestUser();
        }

        [SetUp]
        public void SetUp()
        {
            CountDownApp.GoToHomePage();
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void Should_Display_The_Correct_Owner_Of_The_ToDo_Item()
        {
            // I would put this in a TestCase(..) tag, but attribute arguments cannot be dynamic.
            foreach (var toDoItem in TestToDoItems.ToDoItems)
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                string expectedOutput = String.Format("Owned by: {0}, {1} ({2})", toDoItem.Owner.LastName,
                    toDoItem.Owner.FirstName, toDoItem.Owner.Email);
                Assert.That(CountDownApp.EditToDoPage.ToDoItem.Owner, Is.EqualTo(expectedOutput));
                CountDownApp.GoToHomePage();
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void Should_Indicate_That_The_Item_Is_Completed_If_It_Is_Completed()
        {
            CountDownApp.HomePage.ClickToDoItem(TestToDoItems.CompletedToDoItem.Id);
            Assert.That(CountDownApp.EditToDoPage.ToDoItem.IsCompleted, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void Should_Always_Display_The_Cancel_Button()
        {
            foreach (var toDoItem in TestToDoItems.ToDoItems)
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                Assert.That(CountDownApp.EditToDoPage.CancelButton.IsVisible, Is.True);
                CountDownApp.GoToHomePage();
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void By_Default_Should_Disable_All_Fields()
        {
            foreach (var toDoItem in TestToDoItems.ToDoItems)
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                Assert.That(CountDownApp.EditToDoPage.AllFieldsAreDisabled(), Is.True);
                CountDownApp.GoToHomePage();
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void Should_Not_Display_The_Delete_Edit_Or_Submit_Buttons_If_The_Item_Is_Complete()
        {
            CountDownApp.HomePage.ClickToDoItem(TestToDoItems.CompletedToDoItem.Id);
            Assert.That(CountDownApp.EditToDoPage.DeleteButton.IsVisible, Is.False);
            Assert.That(CountDownApp.EditToDoPage.EditButton.IsVisible, Is.False);
            Assert.That(CountDownApp.EditToDoPage.SubmitButton.IsVisible, Is.False);
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void
            Should_Display_The_Delete_And_Edit_Buttons_If_The_Owner_Is_The_Current_User_And_The_Item_Is_Not_Complete
            ()
        {
            foreach (
                var toDoItem in
                    new TestToDoItem[]
                    {
                        TestToDoItems.AssignedToAndOwnedByPrimaryUser,
                        TestToDoItems.AssignedToSecondaryUserOwnedByPrimaryUser
                    })
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                Assert.That(CountDownApp.EditToDoPage.DeleteButton.IsVisible, Is.True);
                Assert.That(CountDownApp.EditToDoPage.EditButton.IsVisible, Is.True);
                CountDownApp.GoToHomePage();
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void
            Should_Not_Display_The_Delete_And_Edit_Buttons_If_The_Owner_Is_Not_The_Current_User_And_The_Item_Is_Not_Complete
            ()
        {
            foreach (
                var toDoItem in
                    new TestToDoItem[]
                    {
                        TestToDoItems.AssignedToAndOwnerBySecondaryUser,
                        TestToDoItems.AssignedToPrimaryUserAndOwnedBySecondaryUser
                    })
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                Assert.That(CountDownApp.EditToDoPage.DeleteButton.IsVisible, Is.False);
                Assert.That(CountDownApp.EditToDoPage.EditButton.IsVisible, Is.False);
                CountDownApp.GoToHomePage();
            }
        }

        [Test]
        [Category("Functional UI Tests: Feature 10")]
        public void Should_Return_To_The_Home_Page_When_The_Cancel_Button_Is_Clicked()
        {
            foreach (var toDoItem in TestToDoItems.ToDoItems)
            {
                CountDownApp.HomePage.ClickToDoItem(toDoItem.Id);
                CountDownApp.EditToDoPage.ClickCancelButton();
                Assert.That(CountDownApp.IsOnHomePage(), Is.True);
            }
        }
    }
}