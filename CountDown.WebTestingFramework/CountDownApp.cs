using CountDown.WebTestingFramework.Config;

namespace CountDown.WebTestingFramework
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public static partial class CountDownApp
    {
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

        public static bool IsOnLoginPage()
        {
            return Browser.Title.Equals("Login");
        }

        public static bool IsOnRegistrationPage()
        {
            return Browser.Title.Equals("Registration");
        }

        public static bool IsOnHomePage()
        {
            return Browser.Title.Equals("Home");
        }

        public static bool IsOnCreateToDoPage()
        {
            return Browser.Title.Equals("Create To-Do Item");
        }

        public static bool IsOnEditToDoPage()
        {
            return Browser.Title.Equals("Edit To-Do Item");
        }

        public static void GoToHomePage()
        {
            Browser.GoToUrl(Routes.Home);
            Browser.WaitForPageLoad();
        }

        public static void GoToRegistrationPage()
        {
            Browser.GoToUrl(Routes.Register);
            Browser.WaitForPageLoad();
        }

        public static void GoToCreateToDoPage()
        {
            Browser.GoToUrl(Routes.CreateToDo);
            Browser.WaitForPageLoad();
        }
    }
}