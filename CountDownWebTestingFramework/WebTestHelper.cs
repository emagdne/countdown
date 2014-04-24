using System;
using System.Text;

namespace CountDownWebTestingFramework
{
    public static class WebTestHelper
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length = 10)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
