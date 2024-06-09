using Project.DTO;
using Project.Model;

namespace Project.DAL
{
    public interface ILotteryDal
    {
        Task<List<Lottery>> GetPresentsAsync();
        Task<User> LotteryUser(Present present);
    }
}