using Microsoft.AspNetCore.Mvc;
using Project.DAL;
using Project.Model;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Project.DTO;

namespace Project.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _config;

        public UserService(IUserDal userDal, IConfiguration config)
        {
            _userDal = userDal;
            _config = config;
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userDal.GetAsync();
        }
        public async Task<User> GetUserAsync(int id)
        {
            return await _userDal.GetUserAsync(id);
        }

        public async Task<string> Login(UserLogin userLogin, HttpContext httpContext)
        {
            User user = await _userDal.Login(userLogin.UserName, userLogin.Password);
            if (user != null)
            {
               return  GenerateJSONWebToken(user, httpContext);
            }
            return "";
        }

        public async Task<bool> Register(User user)
        {
            
               return await _userDal.Register(user); 
        }


        private string GenerateJSONWebToken(User user,HttpContext httpContext)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.PrimaryGroupSid,user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.IsAdmin.ToString()),
                new Claim(ClaimTypes.StreetAddress,user?.Address),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.OtherPhone,user.PhonNumber),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            var middlewareUser = httpContext.Items["User"] as User;

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
