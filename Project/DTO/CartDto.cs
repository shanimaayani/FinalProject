using System.ComponentModel;

namespace Project.DTO
{
    public class CartDto
    {
        public int UserId { get; set; }
        public int Sum { get; set; }
        [DefaultValue(false)]
        public bool IsClose { get; set; }
    }
}
