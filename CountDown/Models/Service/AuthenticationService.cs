using System.Web.Security;

namespace CountDown.Models.Service
{
    public interface IAuthenticationService
    {
        bool ValidateUser(string username, string password);
        void HandleLoginRedirect(string username, bool rememberMe);
    }

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