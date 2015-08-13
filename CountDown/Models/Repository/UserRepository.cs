using System.Collections.Generic;
using System.Linq;
using CountDown.Models.Domain;
using Microsoft.AspNet.Identity;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public interface IUserRepository
    {
        void InsertUser(User user);
        IEnumerable<User> AllUsersByLastNameFirstName();
        void SaveChanges();
    }

    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _db;
        private readonly PasswordHasher _passwordHasher;

        public UserRepository()
        {
            _db = new UserContext();
            _passwordHasher = new PasswordHasher();
        }

        public void InsertUser(User user)
        {
            // Hash the password before persisting to DB
            user.Hash = _passwordHasher.HashPassword(user.Password);
            _db.Users.Add(user);
        }

        public IEnumerable<User> AllUsersByLastNameFirstName()
        {
            return _db.Users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}