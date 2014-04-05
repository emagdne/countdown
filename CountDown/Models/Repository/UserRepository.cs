using CountDown.Models.Domain;
using Microsoft.AspNet.Identity;

namespace CountDown.Models.Repository
{
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

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}