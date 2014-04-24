using CountDownWebTestingFramework;
using NUnit.Framework;

namespace CountDownFunctionalTests
{
    [TestFixture]
    public class The_Login_Page
    {
        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_countdown_login()
        {
            CountDown.GoToLoginPage();
            Assert.That(CountDown.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_countdown()
        {
            CountDown.Logout();
            CountDown.GoToIndexPage();
            Assert.That(CountDown.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Have_A_Register_Button()
        {
            CountDown.GoToLoginPage();
            Assert.That(CountDown.LoginPage.HasRegisterButton, Is.True);
        }


        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Registration_Page_When_The_Register_Button_Is_Clicked_From_The_Login_Page()
        {
            CountDown.GoToLoginPage();
            CountDown.LoginPage.RegisterButton.Click();
            Assert.That(CountDown.IsOnRegistrationPage, Is.True);
        }
    }
}
