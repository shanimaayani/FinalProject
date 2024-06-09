using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Project.DTO;
using AutoMapper;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IConfiguration config,IMapper mapper)
        {
            _userService = userService;
            _config = config;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [Authorize(Roles = "True")]
        [HttpGet]
        public async Task<List<UserDto>> GetAsync()
        {
            var users = await _userService.GetAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDto> GetUserAsync(int id)
        {
            var user= await _userService.GetUserAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Post( [FromBody] UserLogin userLogin )
        {
            //IActionResult response = Unauthorized();
            var tokenString = await _userService.Login(userLogin, HttpContext);
            if (tokenString != "")
            {
                return Ok(new { token = tokenString }) ;
            }
            return Unauthorized (new { Unauthorized = "Unauthorized" }) ;
        }

        [HttpPost]
        [Route("Rejister")]
        public async Task<bool>  Post([FromBody] User user)
        {
            return await _userService.Register(user);
        }

        

    }
}



