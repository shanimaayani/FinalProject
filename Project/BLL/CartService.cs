using Project.DAL;
using Project.Model;

namespace Project.BLL
{
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;

        public CartService(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _cartDal.GetCartsAsync();
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await _cartDal.GetCartAsync(id);
        }

        public async Task<int> AddCartAsync(Cart cart)
        {
            return await _cartDal.AddCartAsync(cart);
        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            return await _cartDal.DeleteCartAsync(id);
        }

        public async Task<int> GetNumOfpurchaser(int presentId)
        {
            return await _cartDal.GetNumOfpurchaser(presentId);
        }

        public async Task<List<Present>> FilterMaxAcquired()
        {
            return await _cartDal.FilterMaxAcquired();
        }
        public async Task<List<Present>> FilterMaxPrice()
        {
            return await _cartDal.FilterMaxPrice();
        }
        public async Task<List<User>> GetUsers()
        {
            return await _cartDal.GetUsers();
        }
        public async Task<Cart> UpdateCartAsync(int cartId)
        {
            return await _cartDal.UpdateCartAsync(cartId);
        }

        public async Task<int> GetSumOfCarts()
        {
            return await _cartDal.GetSumOfCarts();
        }
    }
}
