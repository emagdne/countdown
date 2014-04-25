using CountDown.FunctionalTests.Config;
using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests
{
    [SetUpFixture]
    public class AllTestsSetUp
    {
        [SetUp]
        public void RunOnceBeforeAllTests()
        {
            CountDownApp.Init();
            CountDownDatabase.OpenConnection();
            CountDownDatabase.CreateUser(Configuration.TestUser.FirstName, Configuration.TestUser.LastName,
                Configuration.TestUser.Email, Configuration.TestUser.Hash);
        }

        [TearDown]
        public void RunOnceAfterAllTests()
        {
            CountDownApp.Quit();
            CountDownDatabase.DeleteUser(Configuration.TestUser.Email);
            CountDownDatabase.CloseConnection();
        }
    }
}