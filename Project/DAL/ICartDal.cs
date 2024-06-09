using Project.Model;

namespace Project.DAL
{
    public interface ICartDal
    {
        Task<int> AddCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int id);
        Task<Cart> UpdateCartAsync(int cartId);
        Task<Cart> GetCartAsync(int id);
        Task<List<Cart>> GetCartsAsync();
        Task<int> GetNumOfpurchaser(int presentId);
        Task<List<Present>> FilterMaxAcquired();
        Task<List<Present>> FilterMaxPrice();
        Task<List<User>> GetUsers();
        Task<int> GetSumOfCarts();
    }

}
