using CountDownWebTestingFramework;
using NUnit.Framework;

namespace CountDownFunctionalTests
{
    [SetUpFixture]
    public class AllTestsSetUp
    {
        [SetUp]
        public void RunOnceBeforeAllTests()
        {
            CountDown.Init();
            CountDownDatabase.OpenConnection();
        }

        [TearDown]
        public void RunOnceAfterAllTests()
        {
            CountDown.Quit();
            CountDownDatabase.CloseConnection();
        }
    }
}
