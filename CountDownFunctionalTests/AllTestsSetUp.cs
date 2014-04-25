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
        }

        [TearDown]
        public void RunOnceAfterAllTests()
        {
            CountDownApp.Quit();
            CountDownDatabase.CloseConnection();
        }
    }
}
