//using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Project.DTO;
using Project.Model;

namespace Project.BLL
{
    public interface IUserService
    {
        Task<List<User>> GetAsync();
        Task<User> GetUserAsync(int id);
        Task<string> Login(UserLogin userLogin,HttpContext httpContext);
        Task<bool> Register(User user);
    }
}