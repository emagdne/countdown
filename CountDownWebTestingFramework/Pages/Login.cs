namespace CountDownWebTestingFramework
{
    public static partial class CountDown
    {
        public static class LoginPage
        {
            public static bool HasRegisterButton
            {
                get { return Browser.HasElement("input[value=Register]"); }
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
            }

            public static class MessageArea
            {
                public static string Text
                {
                    get { return Browser.GetText("#login-message"); }
                }
            }
        }
    }
}