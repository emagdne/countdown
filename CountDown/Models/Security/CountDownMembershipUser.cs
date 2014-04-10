using System;
using System.Web.Security;
using CountDown.Models.Domain;

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
    public class CountDownMembershipUser : MembershipUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CountDownMembershipUser(User user)
            : base("CountDownMembershipProvider", user.Email, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}