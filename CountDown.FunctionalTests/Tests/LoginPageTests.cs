using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests.Tests
{
    [TestFixture]
    public class The_Login_Page
    {
        [SetUp]
        public void SetUp()
        {
            CountDownApp.GoToLoginPage();
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_countdown_login()
        {
            Assert.That(CountDownApp.IsOnLoginPage(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Login_Page_For_Url_countdown()
        {
            CountDownApp.GoToHomePage();
            Assert.That(CountDownApp.IsOnLoginPage(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Have_A_Register_Button()
        {
            Assert.That(CountDownApp.LoginPage.HasRegisterButton, Is.True);
        }


        [Test]
        [Category("Functional UI Tests: Feature 1")]
        public void Should_Display_The_Registration_Page_When_The_Register_Button_Is_Clicked_From_The_Login_Page()
        {
            CountDownApp.LoginPage.RegisterButton.Click();
            Assert.That(CountDownApp.IsOnRegistrationPage(), Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Have_A_Login_Button()
        {
            Assert.That(CountDownApp.LoginPage.HasLoginButton, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Use_Password_Field_On_The_Login_Page_For_Password()
        {
            Assert.That(CountDownApp.LoginPage.PasswordField.IsPasswordField, Is.True);
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Report_Error_Message_For_Missing_Email()
        {
            CountDownApp.LoginPage.ClickLogin();
            Assert.That(CountDownApp.LoginPage.EmailField.ErrorMessage, Is.EqualTo("Email address is required."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Report_Error_Message_For_Missing_Password()
        {
            CountDownApp.LoginPage.ClickLogin();
            Assert.That(CountDownApp.LoginPage.PasswordField.ErrorMessage, Is.EqualTo("Password is required."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Report_Error_Message_If_User_Cannot_Be_Authenticated()
        {
            CountDownApp.LoginPage.EmailField.Fill("xyz@gmail.com");
            CountDownApp.LoginPage.PasswordField.Fill("123");
            CountDownApp.LoginPage.ClickLogin();
            Assert.That(CountDownApp.LoginPage.MessageArea.Error,
                Is.EqualTo(
                    "The email address or password could not be verified. Please enter a registered email and password."));
        }

        [Test]
        [Category("Functional UI Tests: Feature 4")]
        public void Should_Redirect_To_The_Home_Page_After_A_Successful_Authentication()
        {
            FunctionalTestHelper.SignInAsTestUser();
            Assert.That(CountDownApp.IsOnHomePage(), Is.True);
        }
    }
}