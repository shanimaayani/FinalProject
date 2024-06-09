using Project.Model;

namespace Project.BLL
{
    public interface ICartItemsService
    {
        Task<bool> DeleteOrderItemsAsync(int cartItemId);
        Task<int> AddOrderItemsAsync(CartItem cartItem);
        Task<List<CartItem>> GetOrderItemsAsync(int cartId);
    }
}