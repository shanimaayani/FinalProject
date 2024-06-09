using Project.Model;

namespace Project.BLL
{
    public interface IPresentService
    {
        Task<int> AddPresntAsync(Present present);
        Task<bool> DeletePresntAsync(int id);
        Task<Present> GetPresentAsync(int id);
        Task<List<Present>> GetPresentsAsync();
        Task<Present> UpdatePresentAsync(Present present);
        Task<bool> DeletePresntsAsync(int[] presents);
        Task<List<Present>> FilterPresent(string? presentName, string? presentDonor, string? presentCategory, int? numOfPurch);
    }
}