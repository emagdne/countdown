﻿using System.Web.Security;
using CountDown.Models.Repository;

namespace CountDown.Models.Security
{
    /// <summary>
    /// http://codeutil.wordpress.com/2013/05/14/forms-authentication-in-asp-net-mvc-4/
    /// </summary>
    public class CountDownMembershipProvider : MembershipProvider
    {
        private readonly IUserRepository _userRepository;

        public CountDownMembershipProvider()
        {
            _userRepository = new UserRepository();
        }

        public CountDownMembershipProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = _userRepository.FindUserByEmail(email);
            if (user == null) return null;

            return new CountDownMembershipUser(user);
        }

        #region Overrides

        public override bool ValidateUser(string email, string password)
        {
            return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && _userRepository.AuthenticateUser(email, password);
        }

        #endregion

        #region Not Implemented

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
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

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
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