using Project.DAL;
using Project.Model;

namespace Project.BLL
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal _donorDal;

        public DonorService(IDonorDal donorDal)
        {
            _donorDal = donorDal;
        }

        public async Task<List<Donor>> GetDonorsAsync()
        {
            return await _donorDal.GetDonorsAsync();
        }

        public async Task<Donor> GetDonorAsync(string id)
        {
            return await _donorDal.GetDonorAsync(id);
        }

        public async Task<string> AddDonorAsync(Donor donor)
        {
            return await _donorDal.AddDonorAsync(donor);
        }

        public async Task<Donor> UpdateDonorAsync(Donor donor)
        {
            return await _donorDal.UpdateDonorAsync(donor);
        }

        public async Task<bool> DeleteDonorAsync(string id)
        {
            return await _donorDal.DeleteDonorAsync(id);
        }

        async Task<List<Present>> IDonorService.GetAllPresentsByDonorIdAsync(string id)
        {
            return await _donorDal.GetAllPresentsByDonorIdAsync(id);
        }

        public async Task<List<Donor>> FilterDonor(string? firstName, string? lastName, string? email, string? present)
        {
            return await _donorDal.FilterDonor(firstName,lastName,email,present);
        }
    }
}
