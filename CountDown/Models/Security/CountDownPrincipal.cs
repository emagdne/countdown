using System.Security.Principal;

namespace CountDown.Models.Security
{
    /// <summary>
    /// http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
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