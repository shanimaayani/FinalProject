//using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class CartDal : ICartDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;
        private readonly ILogger<Present> _logger;

        public CartDal(ChineesOctionContext chineesOctionContext, ILogger<Present> logger)
        {
            _chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
            _logger = logger;
        }
        public async Task<List<Cart>> GetCartsAsync()
        {
            try
            {
                return await _chineesOctionContext.Cart.Where(c=>c.IsClose == true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            try
            {
                return await _chineesOctionContext.Cart.FirstOrDefaultAsync(c => c.Id == id );
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> AddCartAsync(Cart cart)
        {
            try
            {
                var c = await _chineesOctionContext.Cart.AddAsync(cart);
                await _chineesOctionContext.SaveChangesAsync();
                return c.Entity.Id;
            }
            catch
            {
                return -1;
            }

        }
        public async Task<Cart> UpdateCartAsync(int cartId)
        {
            try
            {
                var c = await _chineesOctionContext.Cart.FirstOrDefaultAsync(c => c.Id == cartId);
                if (c != null)
                {
                    c.IsClose = true;
                    _chineesOctionContext.Cart.Update(c);
                    await _chineesOctionContext.SaveChangesAsync();
                    return c;
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            try
            {
                var c = await _chineesOctionContext.Cart.FirstOrDefaultAsync(d => d.Id == id);
                if (c != null)
                {
                    _chineesOctionContext.Cart.Remove(c);
                    await _chineesOctionContext.SaveChangesAsync();
                    return true;
                }
                return false;


            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetNumOfpurchaser(int presentId)
        {
            try
            {
                var num = await _chineesOctionContext.CartItem
               .Include(c => c.Cart)
               .Where(c => c.PresentId == presentId && c.Cart.IsClose == true)
               .SumAsync(c => c.Quantity);
               return num;

            }
            catch
            {
                return -1;
            }
            
        }

        public async Task<List<Present>> FilterMaxPrice()
        {
            try
            {
                return await _chineesOctionContext.Present
                  .OrderByDescending(c => c.Price)
                  .ToListAsync();

            }
            catch
            {
                return null;
            }
            
        }

        public async Task<List<Present>> FilterMaxAcquired()
        {
            try
            {
                var c= await _chineesOctionContext.CartItem
                   .Include(c => c.Present)
                   .Include(c => c.Cart)
                   .Where(c => c.Cart.IsClose == true)
                   .GroupBy(p => p.Present.Id) 
                   .OrderByDescending(p => p.Sum(c=>c.Quantity))
                   .Select(c => c.First().Present)
                   .ToListAsync();
                return c;

            }
            catch
            {
                return null;
            }
           
        }
        public async Task<List<User>> GetUsers()
        {
            try
            {
                var u=await _chineesOctionContext.Cart
                 .Include(c => c.User)
                 .Where(c => c.IsClose == true)
                 .GroupBy(c=>c.UserId)
                 .Select(c => c.First().User)
                 .ToListAsync();
                    return u;

            }
            catch
            {
                return null;
            }
        }


        public async Task<int> GetSumOfCarts()
        {
            var c = await _chineesOctionContext.Cart.Where(c => c.IsClose == true).SumAsync(c => c.Sum);
            return c;
        }


    }
}
