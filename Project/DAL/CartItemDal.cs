using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class CartItemDal : ICartItemDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;
        private readonly ILogger<Present> _logger;

        public CartItemDal(ChineesOctionContext chineesOctionContext, ILogger<Present> logger)
        {
            _chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
            _logger = logger;
        }

        public async Task<List<CartItem>> GetOrderItemsAsync(int cartId)
        {
            try
            {
                return await _chineesOctionContext.CartItem.Include(c => c.Present).Include(c => c.Cart).Where(c => c.CartId == cartId)
                    .Select(c => new CartItem(){Id=c.Id,Present=c.Present,PresentId=c.PresentId,Quantity=c.Quantity,Cart=c.Cart,CartId=c.CartId}).ToListAsync();
                

                
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }

        }

        public async Task<int> AddOrderItemsAsync(CartItem cartItem)
        {
            try
            {
                var cart = await _chineesOctionContext.Cart.FirstOrDefaultAsync(c => c.Id == cartItem.CartId);
                var pres = await _chineesOctionContext.Present.FirstOrDefaultAsync(p=>p.Id==cartItem.PresentId);
                if(cart!=null && pres!=null)
                    cart.Sum += pres.Price;
                 _chineesOctionContext.Cart.Update(cart);
                await _chineesOctionContext.SaveChangesAsync();

                var c = await _chineesOctionContext.CartItem.FirstOrDefaultAsync(c => c.PresentId == cartItem.PresentId && c.CartId==cartItem.CartId);
                if (c == null)
                {
                    cartItem.Quantity = 1;
                    var cr = await _chineesOctionContext.CartItem.AddAsync(cartItem);
                    await _chineesOctionContext.SaveChangesAsync();
                    return cr.Entity.Quantity;
                }
                else if (c != null)
                {
                    c.Quantity++;
                    _chineesOctionContext.CartItem.Update(c);
                    await _chineesOctionContext.SaveChangesAsync();
                    return c.Quantity;

                }
                return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }

        }

        public async Task<bool> DeleteOrderItemsAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _chineesOctionContext.CartItem.Include(c=>c.Cart).FirstOrDefaultAsync(c => c.Id == cartItemId);
                var cart = await _chineesOctionContext.Cart.FirstOrDefaultAsync(c => c.Id == cartItem.CartId);
                if (cart?.IsClose == true) return false;
                var pres = await _chineesOctionContext.Present.FirstOrDefaultAsync(p => p.Id == cartItem.PresentId);
                if (cart != null && pres != null)
                    cart.Sum -= pres.Price;
                _chineesOctionContext.Cart.Update(cart);
                await _chineesOctionContext.SaveChangesAsync();

                var c = await _chineesOctionContext.CartItem.FirstOrDefaultAsync(c => c.Id == cartItemId && c.CartId == cartItem.CartId);
                if (c != null)
                {
                    if (c.Quantity == 1)
                    {
                        _chineesOctionContext.CartItem.Remove(c);
                    }
                    if (c.Quantity > 1)
                    {
                        c.Quantity--;
                        _chineesOctionContext.CartItem.Update(c);
                    }
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



    }
}
