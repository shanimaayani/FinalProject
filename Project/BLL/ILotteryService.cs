using Project.DTO;
using Project.Model;

namespace Project.BLL
{
    public interface ILotteryService
    {
        Task<List<Lottery>> GetPresentsAsync();
        Task<User> LotteryUser(Present present);
    }
}