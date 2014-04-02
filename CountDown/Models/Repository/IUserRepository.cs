using CountDown.Models.Domain;

namespace CountDown.Models.Repository
{
    public interface IUserRepository
    {
        void InsertUser(User user);
        void SaveChanges();
    }
}