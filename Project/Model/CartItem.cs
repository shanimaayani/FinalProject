using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId  { get; set; }
        public int PresentId { get; set; }
        public int Quantity { get; set; }
        public Cart? Cart { get; set; } = null;
        public Present? Present { get; set; } = null;
    }
}