using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests.Tests
{
    [TestFixture]
    public class The_Home_Page
    {
        [TestFixtureSetUp]
        public void SetUpOnce()
        {
            FunctionalTestHelper.SignInAsTestUser();
        }

        [SetUp]
        public void SetUp()
        {
            CountDownApp.GoToIndexPage();
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Have_A_Link_To_Create_A_ToDo_Item()
        {
            Assert.That(CountDownApp.HomePage.HasCreateToDoLink(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 5")]
        public void Should_Display_The_Create_ToDo_Page_When_The_Create_ToDo_Item_Link_Is_Clicked()
        {
            CountDownApp.HomePage.ClickCreateToDoLink();
            Assert.That(CountDownApp.IsOnCreateToDoPage, Is.True);
        }
    }
}
