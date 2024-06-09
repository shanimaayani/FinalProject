using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Net.Mail;
using System.Net;
using Project.DTO;
using AutoMapper;

namespace Project.DAL
{
    public class LotteryDal : ILotteryDal
    {
        private readonly ChineesOctionContext _chineesOctionContext;
        private readonly ILogger<Present> _logger;
        private readonly IMapper _mapper;

        public LotteryDal(ChineesOctionContext chineesOctionContext, ILogger<Present> logger ,IMapper mapper)
        {
            _chineesOctionContext = chineesOctionContext ?? throw new ArgumentNullException(nameof(ChineesOctionContext));
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Lottery>> GetPresentsAsync()
        {
            try
            {
                return await _chineesOctionContext.Lottery
                    .Include(l => l.Present)
                    .Include(l => l.User)
                    .Select(l => new Lottery() { Id = l.Id, Present = l.Present, User = new User { Name=l.User.Name,Email=l.User.Email,PhonNumber=l.User.PhonNumber }, PresentId = l.PresentId, UserId = l.User.Id })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from Donation , the exception" + ex.Message, 1);
                throw new Exception("Logging from Donation , the exception" + ex.Message);
            }
        }

        public async Task<User> LotteryUser(Present present)
        {
            if (await _chineesOctionContext.Lottery.FirstOrDefaultAsync(l => l.PresentId == present.Id) == null)
            {
                List<CartItem> tmplist = new List<CartItem>();

                var cartItemsListTmp = await _chineesOctionContext.CartItem.Where(c => c.PresentId == present.Id).ToListAsync();
                cartItemsListTmp.ForEach(cart =>
                {
                    if (cart != null)
                    {
                        for (int i = 0; i < cart.Quantity; i++) tmplist.Add(cart);
                    }
                });
                if (tmplist.Count != 0)
                {

                    Random r = new Random();
                    int index = r.Next(tmplist.Count);
                    var cartId = tmplist[index].CartId;
                    var user = await _chineesOctionContext.Cart.Include(c=>c.User).Where(c => c.Id == cartId).Select(c=>c.User).FirstOrDefaultAsync();
                    await _chineesOctionContext.Lottery.AddAsync(new Lottery() { PresentId = present.Id, UserId = user.Id });
                    await _chineesOctionContext.SaveChangesAsync();
 
                    return user;
                }
            }
                
                return null;

        }
        public async Task<bool> SendEmail(int userId)
        {
            try
            {
                User user = await _chineesOctionContext.User.FirstOrDefaultAsync(u => u.Id == userId);
                /////////////////
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(user.Email);
                //mailMessage.To.Add("recipient@email.com");
                mailMessage.Subject = "Subject";
                mailMessage.Body = "This is test email";

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = user.Email;
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("username", "password");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email Sent Successfully.");
                return true;
            }
            catch
            {
                return false;
            }
            




        }



    }
}
