using CountDown.FunctionalTests.Data;
using CountDown.WebTestingFramework;
using NUnit.Framework;

namespace CountDown.FunctionalTests
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
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