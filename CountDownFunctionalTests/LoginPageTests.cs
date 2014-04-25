using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests
{
    [TestFixture]
    public class The_Login_Page
    {
        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_CountDownApp_login()
        {
            CountDownApp.GoToLoginPage();
            Assert.That(CountDownApp.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_CountDownApp()
        {
            CountDownApp.Logout();
            CountDownApp.GoToIndexPage();
            Assert.That(CountDownApp.IsOnLoginPage, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Have_A_Register_Button()
        {
            CountDownApp.GoToLoginPage();
            Assert.That(CountDownApp.LoginPage.HasRegisterButton, Is.True);
        }


        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Registration_Page_When_The_Register_Button_Is_Clicked_From_The_Login_Page()
        {
            CountDownApp.GoToLoginPage();
            CountDownApp.LoginPage.RegisterButton.Click();
            Assert.That(CountDownApp.IsOnRegistrationPage, Is.True);
        }
    }
}
