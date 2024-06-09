using Project.Model;

namespace Project.DAL
{
    public interface IPresentDal
    {
        Task<int> AddPresntAsync(Present present);
        Task<bool> DeletePresntAsync(int id);
        Task<Present> GetPresentAsync(int id);
        Task<List<Present>> GetPresentsAsync();
        Task<Present> UpdatePresntAsync(Present present);
        Task<bool> DeletePresntsAsync(int[] presents);
        Task<List<Present>> FilterPresent(string? presentName, string? presentDonor, string? presentCategory,int? numOfPurch);
    }
}