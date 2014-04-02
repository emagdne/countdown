using System.Data.Entity;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    public class UserContext : DbContext
    {
        public UserContext() : base("MySql") { }

        public DbSet<User> Users { get; set; }
    }
}