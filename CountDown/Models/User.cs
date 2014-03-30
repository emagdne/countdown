using System.Data.Entity;

namespace CountDown.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PlainTextPassword { get; set; }
    }

    public class UserContext : DbContext
    {
        public UserContext() : base("MySql") { }

        public DbSet<User> Users { get; set; }
    }
}