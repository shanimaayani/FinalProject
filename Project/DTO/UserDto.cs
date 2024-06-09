using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.DTO
{
    public class UserDto
    {
        public string Name { get; set; }

        [MaxLength(10, ErrorMessage = "Phone Number can't be more than 10 ")]
        [MinLength(9, ErrorMessage = "Phone Number can't be less than 10 ")]
        public string PhonNumber { get; set; }

        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Address can't be more than 50 ")]
        public string? Address { get; set; }

    }
}
