using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserContext _db;

        public UserRepository()
        {
            _db = new UserContext();
        }

        public UserRepository(UserContext context)
        {
            _db = context;
        }

        public void InsertUser(User user)
        {
            // todo: hash the password!
            _db.Users.Add(user);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}