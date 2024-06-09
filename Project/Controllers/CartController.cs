using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.DTO;
using Project.Model;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CartDto>> Get()
        {
            var carts = await _cartService.GetCartsAsync();
            return _mapper.Map<List<CartDto>>(carts);

        }
        [Route("GetNumOfpurchaser")]
        [HttpGet]
        public async Task<int> GetNumOfpurchaser(int presentId)
        {
            return await _cartService.GetNumOfpurchaser(presentId);

        }
        [Authorize(Roles = "True")]
        [Route("GetUsers")]
        [HttpGet]
        public async Task<List<UserDto>> GetUsers()
        {
             var users = await _cartService.GetUsers();
             return _mapper.Map<List<UserDto>>(users);

        }

        [Route("FilterMaxAcquired")]
        [HttpGet]
        public async Task<List<PresentDto>> FilterMaxAcquired()
        {
            var present = await _cartService.FilterMaxAcquired();
            return _mapper.Map<List<PresentDto>>(present);

        }


        [Route("FilterMaxPrice")]
        [HttpGet]
        public async Task<List<PresentDto>> FilterMaxPrice()
        {
                var present = await _cartService.FilterMaxPrice();
                var p= _mapper.Map<List<PresentDto>>(present);
                return p;

        }
        [Authorize(Roles = "True")]
        [Route("GetSumOfCarts")]
        [HttpGet]
        public async Task<int> GetSumOfCarts()
        {
           return await _cartService.GetSumOfCarts();
        }


        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public async Task<CartDto> Get(int id)
        {
            var cart = await _cartService.GetCartAsync(id);
            return _mapper.Map<CartDto>(cart);
        }

        // POST api/<PresentsController>
        [HttpPost]
        public async Task<int> Post()
        {
            var userIdClaim = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.PrimaryGroupSid);
            if (int.TryParse(userIdClaim?.Value, out int userId))
            {
                var cart = new Cart() { UserId = userId, Sum = 0, IsClose = false };
                var _cart = _mapper.Map<Cart>(cart);
                    return await _cartService.AddCartAsync(_cart);
                
            }
  
            return -1;
        }

        [HttpPut]
        public async Task<CartDto> Put(int cartId)
        {
            var car = await _cartService.UpdateCartAsync(cartId);
            return _mapper.Map<CartDto>(car);
        }


        // DELETE api/<PresentsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _cartService.DeleteCartAsync(id);
        }

        
    }
}
