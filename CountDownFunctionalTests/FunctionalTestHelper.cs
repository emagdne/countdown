using CountDown.FunctionalTests.Config;
using CountDown.WebTestingFramework;

namespace CountDown.FunctionalTests
{
    public static class FunctionalTestHelper
    {
        public static void SignInAsTestUser()
        {
            CountDownApp.GoToLoginPage();
            CountDownApp.LoginPage.EmailField.Fill(Configuration.TestUser.Email);
            CountDownApp.LoginPage.PasswordField.Fill(Configuration.TestUser.Password);
            CountDownApp.LoginPage.ClickLogin();
        }
    }
}
