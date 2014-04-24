using System;

namespace CountDownWebTestingFramework
{
    public static partial class CountDown
    {
        public static class RegistrationPage
        {
            public static class LoginLink
            {
                public static bool Click()
                {
                    return Browser.ClickElement("#navigation > a");
                }
            }

            public static class FirstNameField
            {
                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=FirstName]"); }
                }

                public static void Fill(string value)
                {
                    Browser.FillField("#reg-fn", value);
                }
            }

            public static class LastNameField
            {
                public static void Fill(string value)
                {
                    Browser.FillField("#reg-ln", value);
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=LastName"); }
                }
            }

            public static class EmailField
            {
                public static void Fill(string value)
                {
                    Browser.FillField("#reg-email", value);
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=Email]"); }
                }
            }

            public static class EmailConfirmField
            {
                public static void Fill(string value)
                {
                    Browser.FillField("#reg-email-confirm", value);
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=ConfirmEmail]"); }
                }
            }

            public static class PasswordField
            {
                public static bool IsPasswordField
                {
                    get { return Browser.ElementHasAttributeWithValue("#reg-pw", "type", "password"); }
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=Password]"); }
                }

                public static void Fill(string value)
                {
                    Browser.FillField("#reg-pw", value);
                }
            }

            public static class PasswordConfirmField
            {
                public static bool IsPasswordField
                {
                    get { return Browser.ElementHasAttributeWithValue("#reg-pw-confirm", "type", "password"); }
                }

                public static string ErrorMessage
                {
                    get { return Browser.GetText("span[data-valmsg-for=ConfirmPassword]"); }
                }

                public static void Fill(string value)
                {
                    Browser.FillField("#reg-pw-confirm", value);
                }
            }

            public static void FillFieldsWithRandomValues()
            {
                Browser.FillField("#reg-fn", WebTestHelper.RandomString());
                Browser.FillField("#reg-ln", WebTestHelper.RandomString());
                Browser.FillField("#reg-email", WebTestHelper.RandomString());
                Browser.FillField("#reg-email-confirm", WebTestHelper.RandomString());
                Browser.FillField("#reg-pw", WebTestHelper.RandomString());
                Browser.FillField("#reg-pw-confirm", WebTestHelper.RandomString());
            }

            public static void ClickClear()
            {
                Browser.ClickElement("#reg-clear");
            }

            public static bool AreFieldsBlank()
            {
                var value1 = Browser.GetInputValue("#reg-fn");
                var value2 = Browser.GetInputValue("#reg-ln");
                var value3 = Browser.GetInputValue("#reg-email");
                var value4 = Browser.GetInputValue("#reg-email-confirm");
                var value5 = Browser.GetInputValue("#reg-pw");
                var value6 = Browser.GetInputValue("#reg-pw-confirm");
                return String.IsNullOrEmpty(value1 + value2 + value3 + value4 + value5 + value6);
            }

            public static void ClickSubmit()
            {
                Browser.ClickElement("input[type=submit]");
            }
        }
    }
}