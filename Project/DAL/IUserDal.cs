using Project.Model;

namespace Project.DAL
{
    public interface IUserDal
    {
        Task<List<User>> GetAsync();
        Task<User> GetUserAsync(int id);
        Task<User> Login(string userName, string password);
        Task<bool> Register(User user);
    }
}