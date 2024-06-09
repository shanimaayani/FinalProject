using Project.Model;

namespace Project.BLL
{
    public interface IDonorService
    {
        Task<string> AddDonorAsync(Donor donor);
        Task<bool> DeleteDonorAsync(string id);
        Task<Donor> GetDonorAsync(string id);
        Task<List<Donor>> GetDonorsAsync();
        Task<Donor> UpdateDonorAsync(Donor donor);
        Task<List<Present>> GetAllPresentsByDonorIdAsync(string id);
        Task<List<Donor>> FilterDonor(string? firstName, string? lastName, string? email, string? present);
    }
}