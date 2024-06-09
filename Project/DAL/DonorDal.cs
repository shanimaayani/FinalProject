
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Project.Model;
using System.Drawing;

namespace Project.DAL
{
    public class DonorDal : IDonorDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;

        public DonorDal(ChineesOctionContext chineesOctionContext)
        {
            _chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
        }
        public async Task<List<Donor>> GetDonorsAsync()
        {
            try
            {
                return await _chineesOctionContext.Donor.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Donor> GetDonorAsync(string id)
        {
            try
            {
                return await _chineesOctionContext.Donor.FirstOrDefaultAsync(d => d.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> AddDonorAsync(Donor donor)
        {
            try
            {
                var d = await _chineesOctionContext.Donor.AddAsync(donor);
                await _chineesOctionContext.SaveChangesAsync();
                return d.Entity.Id;
            }
            catch
            {
                return "";
            }

        }

        public async Task<Donor> UpdateDonorAsync(Donor donor)
        {
            try
            {
                var d = await _chineesOctionContext.Donor.FirstOrDefaultAsync(d => d.Id == donor.Id);
                if (d != null)
                {
                    d.FirstName = donor.FirstName;
                    d.LastName = donor.LastName;
                    d.Email = donor.Email;
                    d.Address = donor.Address;
                    _chineesOctionContext.Donor.Update(d);
                    await _chineesOctionContext.SaveChangesAsync();
                }

                return d;

            }
            catch
            {
                return null;
            }


        }
        public async Task<bool> DeleteDonorAsync(string id)
        {
            try
            {
                var d = await _chineesOctionContext.Donor.FirstOrDefaultAsync(d => d.Id == id);
                var p = await _chineesOctionContext.Present.FirstOrDefaultAsync(p => p.DonorId == id);
                if (p != null) return false;
                if (d != null)
                {
                    _chineesOctionContext.Donor.Remove(d);
                    _chineesOctionContext.SaveChanges();
                    return true;
                }
                return false;


            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Present>> GetAllPresentsByDonorIdAsync(string id)
        {
             try
             {
                return await _chineesOctionContext.Present
                   .Include(d => d.Donor).
                    Where(d => d.DonorId == id).
                    Select(p => p)
                   .ToListAsync();
             }
             catch
             {
                return null;
             }
        }


        public async Task<List<Donor>> FilterDonor(string? firstName, string? lastName, string? email,string? presentName) 
        {
            try

            {
                if (presentName != null)
                {
                    return await _chineesOctionContext.Present
                    .Include(p => p.Donor)
                    .Where(p => p.Name == presentName)
                    .Select(p => p.Donor)
                    .ToListAsync();
                    
                }
                return await _chineesOctionContext.Donor
                 .Where(donor =>
                ((firstName == null) ? (true) : (donor.FirstName.Contains(firstName)))
                && ((lastName == null) ? (true) : (donor.LastName.Contains(lastName)))
                && ((email == null) ? (true) : (donor.Email.Contains(email))))
                .ToListAsync();
            }
            catch
            {
                return null;
            }
        }


    }
}
