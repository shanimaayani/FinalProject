using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ChineesOctionContext : DbContext
    {
        public ChineesOctionContext(DbContextOptions<ChineesOctionContext> option ):base(option){}
        public DbSet<User> User { get; set; }
        public DbSet<Present> Present { get; set; }
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Lottery> Lottery { get; set; }
    }
}
