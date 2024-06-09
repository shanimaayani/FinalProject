using Project.DAL;
using Project.Model;

namespace Project.BLL
{
    public class CartItemsService : ICartItemsService
    {
        private readonly ICartItemDal _cartItemDal;

        public CartItemsService(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }
        public async Task<List<CartItem>> GetOrderItemsAsync(int cartId)
        {
            return await _cartItemDal.GetOrderItemsAsync(cartId);
        }

        public async Task<int> AddOrderItemsAsync(CartItem cartItem)
        {
            return await _cartItemDal.AddOrderItemsAsync(cartItem);
        }


        public async Task<bool> DeleteOrderItemsAsync(int cartItemId)
        {
            return await _cartItemDal.DeleteOrderItemsAsync(cartItemId);
        }
    }
}
