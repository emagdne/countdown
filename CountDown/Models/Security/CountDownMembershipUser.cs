using System;
using System.Web.Security;
using CountDown.Models.Domain;

namespace CountDown.Models.Security
{
    /// <summary>
    /// http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
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