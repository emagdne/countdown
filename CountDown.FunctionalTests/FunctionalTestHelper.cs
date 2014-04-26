using System;
using System.Text;
using CountDown.FunctionalTests.Config;
using CountDown.WebTestingFramework;

namespace CountDown.FunctionalTests
{
    public static class FunctionalTestHelper
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length = 10)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static void SignInAsTestUser()
        {
            CountDownApp.GoToLoginPage();
            CountDownApp.LoginPage.EmailField.Fill(Configuration.TestUser.Email);
            CountDownApp.LoginPage.PasswordField.Fill(Configuration.TestUser.Password);
            CountDownApp.LoginPage.ClickLogin();
        }
    }
}
