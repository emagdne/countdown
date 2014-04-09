﻿using System;
using System.Security.Principal;
using System.Web.Security;

namespace CountDown.Models.Security
{
    /// <summary>
    /// http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
    /// </summary>
    [Serializable]
    public class CountDownIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Name
        {
            get { return Identity.Name; }
        }

        public string AuthenticationType { get { return Identity.AuthenticationType; } }

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        public CountDownIdentity(IIdentity identity)
        {
            Identity = identity;
            var membershipUser = Membership.GetUser(identity.Name) as CountDownMembershipUser;
            if (membershipUser != null)
            {
                Id = membershipUser.Id;
                FirstName = membershipUser.FirstName;
                LastName = membershipUser.LastName;
                Email = membershipUser.Email;
            }
        }
    }
}