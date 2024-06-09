using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Sum { get; set; }

        [DefaultValue(false)]
        public bool IsClose { get; set; }
        public User? User { get; set; } = null;
        public ICollection<CartItem>? MyItems { get; set; } = null;
    }
}
