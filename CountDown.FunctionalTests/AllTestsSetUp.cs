using CountDown.FunctionalTests.Data;
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
            TestDataManager.GenerateTestData();
        }

        [TearDown]
        public void RunOnceAfterAllTests()
        {
            CountDownApp.Quit();
            TestDataManager.DestroyTestData();
            CountDownDatabase.CloseConnection();
        }
    }
}