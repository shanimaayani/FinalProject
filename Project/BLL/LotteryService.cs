using Project.DAL;
using Project.DTO;
using Project.Model;

namespace Project.BLL
{
    public class LotteryService : ILotteryService
    {
        private readonly ILotteryDal _lotteryDal;

        public LotteryService(ILotteryDal lotteryDal)
        {
            _lotteryDal = lotteryDal;
        }

        public async Task<List<Lottery>> GetPresentsAsync()
        {
            return await _lotteryDal.GetPresentsAsync();
        }

        public async Task<User> LotteryUser(Present present)
        {
            return await _lotteryDal.LotteryUser(present);
        }
    }
}
