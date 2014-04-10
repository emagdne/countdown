using System.Data.Entity;
using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class UserContext : DbContext
    {
        public UserContext() : base("MySql") { }

        public DbSet<User> Users { get; set; }
    }
}