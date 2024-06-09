using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Project.DAL;
using Project.Model;

namespace Project.BLL
{
    public class PresentService : IPresentService
    {
        private readonly IPresentDal _presentDal;

        public PresentService(IPresentDal presentDal)
        {
            _presentDal = presentDal;
        }

        public async Task<List<Present>> GetPresentsAsync()
        {
            return await _presentDal.GetPresentsAsync();
        }

        public async Task<Present> GetPresentAsync(int id)
        {
            return await _presentDal.GetPresentAsync(id);
        }

        public async Task<int> AddPresntAsync(Present present)
        {
            var pres = new Present() { Price = present.Price, Name = present.Name, Category = present.Category, DonorId = present.DonorId ,Picture=present.Picture};
            return await _presentDal.AddPresntAsync(pres);
        }

        public async Task<Present> UpdatePresentAsync(Present present)
        {
            return await _presentDal.UpdatePresntAsync(present);
        }

        public async Task<bool> DeletePresntAsync(int id)
        {
            return await _presentDal.DeletePresntAsync(id);
        }
        public async Task<bool> DeletePresntsAsync(int[] presents)
        {
            return await _presentDal.DeletePresntsAsync(presents);
        }
        public async Task<List<Present>> FilterPresent(string? presentName, string? presentDonor, string? presentCategory, int? numOfPurch)
        {
                return await _presentDal.FilterPresent(presentName,presentDonor, presentCategory, numOfPurch);
        }
    }
}
