using System.Web.Security;

namespace CountDown.Models.Service
{
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// <para>Enables unit testing of the authentication system.</para>
    public interface IAuthenticationService
    {
        bool ValidateUser(string username, string password);
        void HandleLoginRedirect(string username, bool rememberMe);
    }

    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    public class AuthenticationService : IAuthenticationService
    {
        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }

        public void HandleLoginRedirect(string username, bool rememberMe)
        {
            FormsAuthentication.RedirectFromLoginPage(username, rememberMe);
        }
    }
}