using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }


        // GET: api/<UserController>
        [HttpGet]
        [Route("getAll")]
        public async Task<List<User>> GetAsync()
        {
            return await _userService.GetAsync();
        }


        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _userService.GetUserAsync(id);
        }

       
        [HttpPost]
        [Route("Login")]
        public async Task<bool> Login([FromBody] UserLogin userlogin)
        {
            
           return await _userService.Login(userlogin.UserName,userlogin.Password);
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<bool> Register([FromBody] User user)
        {
           return await _userService.Register(user);
      
        }



    }      
}
