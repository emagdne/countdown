using System;
using System.Text;
using CountDown.FunctionalTests.Data.TestData;
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
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static void SignInAsTestUser()
        {
            CountDownApp.GoToLoginPage();
            CountDownApp.LoginPage.EmailField.Fill(TestUsers.PrimaryUser.Email);
            CountDownApp.LoginPage.PasswordField.Fill(TestUsers.PrimaryUser.Password);
            CountDownApp.LoginPage.ClickLogin();
        }

        public static long[] CreateToDoItems(int count, string title, string description)
        {
            long[] ids = new long[count];

            for (int i = 0; i < count; i++)
            {
                // Add unique number to title and description to ensure a clean delete.
                ids[i] = CountDownDatabase.CreateToDoItem(title + " #" + (i + 1), description + " #" + (i + 1),
                    DateTime.Now, DateTime.Now.AddDays(1),
                    TestUsers.PrimaryUser.Id, TestUsers.PrimaryUser.Id, false);
            }

            return ids;
        }

        public static void DeleteToDoItems(long[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                CountDownDatabase.DeleteToDoItem(ids[i]);
            }
        }
    }
}