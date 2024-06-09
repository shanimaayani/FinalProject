using System.ComponentModel.DataAnnotations;

namespace Project.DTO
{
    public class DonorDto
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
