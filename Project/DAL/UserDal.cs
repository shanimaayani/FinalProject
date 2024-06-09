using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Project.Model;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;

namespace Project.DAL
{
    public class UserDal : IUserDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;


        public UserDal(ChineesOctionContext chineesOctionContext)
        {
            this._chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
           
        }

        

        public async Task<List<User>> GetAsync()
        {

            return await _chineesOctionContext.User.ToListAsync();

        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _chineesOctionContext.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _chineesOctionContext.User.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> Register(User user)
        {
            try {
                
                await _chineesOctionContext.User.AddAsync(user);
                await _chineesOctionContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        

    }
}
