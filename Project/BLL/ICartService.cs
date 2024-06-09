using Project.Model;

namespace Project.BLL
{
    public interface ICartService
    {
        Task<int> AddCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int id);
        Task<Cart> GetCartAsync(int id);
        Task<Cart> UpdateCartAsync(int cartId);
        Task<List<Cart>> GetCartsAsync();
        Task<int> GetNumOfpurchaser(int presentId);
        Task<List<Present>> FilterMaxAcquired();
        Task<List<Present>> FilterMaxPrice();
        Task<List<User>> GetUsers();
        Task<int> GetSumOfCarts();

    }
}