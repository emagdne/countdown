using System.Linq;
using System.Web.Security;
using CountDown.Models.Repository;
using Microsoft.AspNet.Identity;

namespace CountDown.Models.Security
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// <para>
    ///     Module needed for the authentication system.
    /// </para>
    /// <para>
    ///     Design taken from this article:
    ///     http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
    /// </para>
    /// </summary>
    public class CountDownMembershipProvider : MembershipProvider
    {
        public CountDownMembershipProvider()
        {
        }

        #region Overrides

        /* 
         * The methods below access UserContext directly for a good reason. I tried instantiating UserRepository
         * here, but Entity Framework threw some strange exceptions due to the fact that UserRepository kept
         * a DbContext object in memory over the span of multiple requests.
         *
         * For future reference, the main exception thrown was EntityCommandExecutionException, followed by a variety
         * of inner exceptions such as SQLite's "library routine called out of sequence\r\nnull connection or database handle".
         */

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email.Equals(email));
                return user == null ? null : new CountDownMembershipUser(user);
            }
        }

        public override bool ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return false;
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email.Equals(email));
                var verification = PasswordVerificationResult.Failed;
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher();
                    verification = passwordHasher.VerifyHashedPassword(user.Hash, password);
                }
                return user != null && (verification == PasswordVerificationResult.Success
                                        || verification == PasswordVerificationResult.SuccessRehashNeeded);
            }
        }

        #endregion

        #region Not Implemented

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new System.NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}