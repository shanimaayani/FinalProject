using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Project.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Name can't be more than 50 ")]
        public string Name { get; set; }

        [MaxLength(10, ErrorMessage = "User Name Number can't be more than 10")]
        [MinLength(3, ErrorMessage = "User Name Number can't be less than 3 ")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [MaxLength(10,ErrorMessage ="Phone Number can't be more than 10 ")]
        [MinLength(9, ErrorMessage ="Phone Number can't be less than 10 ")]
        public string PhonNumber { get; set; }

        //[EmailAddress]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Address can't be more than 50 ")]
        public string? Address { get; set; }

        [DefaultValue(false)]
        public bool? IsAdmin { get; set; } = false;
       
    }
}
