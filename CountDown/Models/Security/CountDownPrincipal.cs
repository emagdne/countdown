using System.Security.Principal;

namespace CountDown.Models.Security
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// <para>
    ///     Module needed for the authentication system.
    /// </para>
    /// <para>
    ///     Design taken from this article:
    ///     http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
    /// </para>
    /// </summary>
    public class CountDownPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity { get; private set; }

        public CountDownIdentity CountDownIdentity
        {
            get { return (CountDownIdentity) Identity; }
            set { Identity = value; }
        }

        public CountDownPrincipal(CountDownIdentity identity)
        {
            Identity = identity;
        }
    }
}