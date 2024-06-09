using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Lottery
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PresentId { get; set; }
        public User User { get; set; }
        public Present Present { get; set; }

    }
}
