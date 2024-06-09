using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.Model;

namespace Project.DAL
{
    public class PresentDal : IPresentDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;
        private readonly ILogger<Present> _logger;

        public PresentDal(ChineesOctionContext chineesOctionContext,ILogger<Present> logger)
        {
            _chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
            _logger = logger;
        }

        public async Task<List<Present>> GetPresentsAsync()
        {
            try
            {
                return await _chineesOctionContext.Present.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }

        public async Task<Present> GetPresentAsync(int id)
        {
            try
            {
                return await _chineesOctionContext.Present.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }

        public async Task<int> AddPresntAsync(Present present)
        {
            try
            {
                var p = await _chineesOctionContext.Present.AddAsync(present);
                await _chineesOctionContext.SaveChangesAsync();
                return p.Entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }

        }

        public async Task<Present> UpdatePresntAsync(Present present)
        {
            try
            {
                var p = await _chineesOctionContext.Present.FirstOrDefaultAsync(p => p.Id == present.Id);
                if (p != null)
                {
                    p.Name = present.Name;
                    p.Price = present.Price;
                    p.Category = present.Category;
                    p.DonorId = present.DonorId;
                    _chineesOctionContext.Present.Update(p);
                    await _chineesOctionContext.SaveChangesAsync();
                    return p;
                }
                return null;
                

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }


        }
        public async Task<bool> DeletePresntAsync(int id)
        {
            try
            {
                var p = await _chineesOctionContext.Present.FirstOrDefaultAsync(p => p.Id == id);
                if (p!=null) 
                {
                    var isTaken = await _chineesOctionContext.CartItem.Include(c => c.Cart).Where(c => c.PresentId == id && c.Cart.IsClose == true).ToListAsync();
                    if (isTaken != null) return false;
                    _chineesOctionContext.Present.Remove(p);
                    await _chineesOctionContext.SaveChangesAsync();
                    return true;
                }
                return false;
               
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }


        }

        public async Task<List<Present>> FilterPresent(string? presentName , string? presentDonor,string? presentCategory,int? numOfPurch)
        {
            try
            {
               
                if (numOfPurch != null)
                {
                    var p = await _chineesOctionContext.CartItem
                   .Include(c => c.Cart)
                   .Include(c => c.Present)
                   .GroupBy(c => c.CartId)
                   .Where(c => (c.Sum(c1 => c1.Quantity)) == numOfPurch)
                   .Select(c => c.FirstOrDefault(c1=>c1.Cart.IsClose==true).Present)
                   .ToListAsync();
                    return p;

                }
                return await _chineesOctionContext.Present
                .Include(p => p.Donor)
                .Where(present =>
                ((presentName == null) ? (true) : (present.Name.Contains(presentName)))
                && ((presentDonor == null) ? (true) : (present.Donor.FirstName.Contains(presentDonor))
                || (present.Donor.LastName.Contains(presentDonor)))
                && ((presentCategory == null) ? (true) : (present.Category.Contains(presentCategory))))
               .ToListAsync();


            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
            
        }
       
        public async Task<bool> DeletePresntsAsync(int[] presents)
        {
            try
            {
                bool flag = false;
                for (int i = 0; i < presents.Length; i++)
                {
                    flag = await DeletePresntAsync(presents[i]);

                }
                return flag;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }




    }
}
