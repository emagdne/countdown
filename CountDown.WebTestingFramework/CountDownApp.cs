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

        public static bool IsOnHomePage
        {
            get { return Browser.Title.Equals("Home"); }
        }

        public static bool IsOnCreateToDoPage
        {
            get { return Browser.Title.Equals("Create To-Do Item"); }
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

        public static void Logout()
        {
            GoToLoginPage();
        }

        public static void GoToHomePage()
        {
            Browser.GoToUrl(Routes.Home);
        }

        public static void GoToRegistrationPage()
        {
            Browser.GoToUrl(Routes.Register);
        }

        public static void GoToCreateToDoPage()
        {
            Browser.GoToUrl(Routes.CreateToDo);
        }
    }
}