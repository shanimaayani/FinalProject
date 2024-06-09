using Project.Model;

namespace Project.DAL
{
    public interface ICartItemDal
    {
        Task<int> AddOrderItemsAsync(CartItem cartItem);
        Task<bool> DeleteOrderItemsAsync(int cartItemId);
        Task<List<CartItem>> GetOrderItemsAsync(int cartId);
    }
}