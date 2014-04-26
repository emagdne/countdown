namespace CountDown.WebTestingFramework
{
    public static partial class CountDownApp
    {
        public static class LoginPage
        {
            public static bool HasRegisterButton
            {
                get { return Browser.HasElement("input[value=Register]"); }
            }

            public static bool HasLoginButton
            {
                get { return Browser.HasElement("input[value=Login]"); }
            }

            public static class RegisterButton
            {
                public static bool Click()
                {
                    return Browser.ClickElement("input[value=Register]");
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
                Browser.ClickElement("input[value=Login]");
            }
        }
    }
}