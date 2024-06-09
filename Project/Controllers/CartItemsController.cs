using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.DTO;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemsService _cartItemsService;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemsService cartItemsService,IMapper mapper)
        {
            _cartItemsService = cartItemsService;
            _mapper = mapper;
        }
        // GET: api/<CartItemsController>
        [HttpGet]
        public async Task<List<CartItem>> Get(int cartId)
        {
            return await _cartItemsService.GetOrderItemsAsync(cartId);
            //return _mapper.Map<List<Cart>>(cartItems);
        }

        // POST api/<CartItemsController>
        [HttpPost]
        public async Task<int> Post([FromBody] CartItemDto cartItem)
        {
            var _cartItem = _mapper.Map<CartItem>(cartItem);
            if (ModelState.IsValid)
            {
                return await _cartItemsService.AddOrderItemsAsync(_cartItem);
            }
            return -1;
        }

        // DELETE api/<CartItemsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _cartItemsService.DeleteOrderItemsAsync(id);
        }
    }
}
