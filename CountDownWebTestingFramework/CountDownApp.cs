using CountDown.WebTestingFramework.Config;

namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static bool IsOnLoginPage
        {
            get { return Browser.Title.Equals("Login"); }
        }

        public static bool IsOnRegistrationPage
        {
            get { return Browser.Title.Equals("Registration"); }
        }

        public static void Init()
        {
            Browser.Init();
        }

        public static void Quit()
        {
            Browser.Quit();
        }

        public static void GoToLoginPage()
        {
            Browser.GoToUrl(Routes.Login);
        }

        public static bool Logout()
        {
            return Browser.Logout();
        }

        public static void GoToIndexPage()
        {
            Browser.GoToUrl(Routes.Home);
        }

        public static void GoToRegistrationPage()
        {
            Browser.GoToUrl(Routes.Register);
        }
    }
}