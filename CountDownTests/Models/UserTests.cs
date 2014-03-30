using CountDown.Models;
using NUnit.Framework;

namespace CountDownTests.Models
{
    [TestFixture]
    public class A_User_Object
    {
        private User _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new User();
        }
    }
}
