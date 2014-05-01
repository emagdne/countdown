using System.Data.Entity;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class UserContext : DbContext
    {
        public UserContext() : base("SQLite") { }

        public DbSet<User> Users { get; set; }
    }
}