namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static class LoginPage
        {
            public static bool HasRegisterButton
            {
                get { return Browser.HasElement("#register-button"); }
            }

            public static bool HasLoginButton
            {
                get { return Browser.HasElement("#login-button"); }
            }

            public static class RegisterButton
            {
                public static void Click()
                {
                    Browser.ClickElement("#register-button");
                    Browser.WaitForPageLoad();
                }
            }

            public static class EmailField
            {
                public static string Value
                {
                    get { return Browser.GetInputValue("input[name=email]"); }
                }

                public static string ErrorMessage { get { return Browser.GetText("span[data-valmsg-for=Email]"); } }

                public static void Fill(string email)
                {
                    Browser.FillInputField("input[name=email]", email);
                }
            }

            public static class MessageArea
            {
                public static string Text
                {
                    get { return Browser.GetText("#login-message"); }
                }

                public static string Error
                {
                    get { return Browser.GetText("#login-error"); }
                }
            }

            public static class PasswordField
            {
                public static bool IsPasswordField
                {
                    get { return Browser.ElementHasAttributeWithValue("input[name=password]", "type", "password"); }
                }

                public static string ErrorMessage { get { return Browser.GetText("span[data-valmsg-for=Password]"); } }

                public static void Fill(string email)
                {
                    Browser.FillInputField("input[name=password]", email);
                }
            }

            public static void ClickLogin()
            {
                Browser.ClickElement("#login-button");
                Browser.WaitForPageLoad();
            }
        }
    }
}