using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Donor
    {
        [Key]
        //[StringLength(10,MinimumLength =9)]
        public string Id { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }


    }
}
