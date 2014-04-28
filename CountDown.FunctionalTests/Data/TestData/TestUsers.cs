using System.Collections.Generic;
using CountDown.WebTestingFramework;

namespace CountDown.FunctionalTests.Data.TestData
{
    public static class TestUsers
    {
        public static TestUser PrimaryUser { get; private set; }

        public static TestUser SecondaryUser { get; private set; }

        public static List<TestUser> Users { get; private set; }

        public static void SetUp()
        {
            Users = new List<TestUser>
            {
                new TestUser
                {
                    FirstName = "Tester",
                    LastName = "Bob",
                    Email = "bob@generatedByTestUsers.cs",
                    Hash = "APTazTWxHZP2/eR0Rm6VkDoXVST1hFJMY/hRYELD5KDQa56Tgz+SWybFMDBVK0Mijw==",
                    Password = "12345"
                },
                new TestUser
                {
                    FirstName = "Tester",
                    LastName = "Joe",
                    Email = "joe@generatedByTestUsers.cs",
                    Hash = "APTazTWxHZP2/eR0Rm6VkDoXVST1hFJMY/hRYELD5KDQa56Tgz+SWybFMDBVK0Mijw==",
                    Password = "12345"
                }
            };

            foreach (var user in Users)
            {
                user.Id = CountDownDatabase.CreateUser(user.FirstName, user.LastName, user.Email, user.Hash);
            }

            PrimaryUser = Users[0];
            SecondaryUser = Users[1];
        }

        public static void CleanUp()
        {
            foreach (var user in Users)
            {
                CountDownDatabase.DeleteUser(user.Id);
            }

            PrimaryUser = null;
            SecondaryUser = null;
            Users = null;
        }
    }
}