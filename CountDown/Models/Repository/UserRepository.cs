using System.Linq;
using CountDown.Models.Domain;
using Microsoft.AspNet.Identity;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public interface IUserRepository
    {
        void InsertUser(User user);
        bool AuthenticateUser(string email, string password);
        User FindUserByEmail(string email);
        IQueryable<User> AllUsers();
        void SaveChanges();
    }

    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserContext _db;

        public UserRepository()
        {
            _db = new UserContext();
            _passwordHasher = new PasswordHasher();
        }

        public UserRepository(UserContext context)
        {
            _db = context;
            _passwordHasher = new PasswordHasher();
        }

        public UserRepository(UserContext context, IPasswordHasher hasher)
        {
            _db = context;
            _passwordHasher = hasher;
        }

        public void InsertUser(User user)
        {
            // Hash the password before persisting to DB
            user.Hash = _passwordHasher.HashPassword(user.Password);
            _db.Users.Add(user);
        }

        public bool AuthenticateUser(string email, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email.Equals(email));
            if (user == null) return false;
            var verification = _passwordHasher.VerifyHashedPassword(user.Hash, password);
            return verification == PasswordVerificationResult.Success
                   || verification == PasswordVerificationResult.SuccessRehashNeeded;
        }

        public User FindUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public IQueryable<User> AllUsers()
        {
            return _db.Users;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}