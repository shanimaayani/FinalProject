using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
   
    public class Present
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
       
        [DefaultValue(10)]
        [Range(10,60)]
        public int Price { get; set; }
        public string Category { get; set; }

        public string DonorId { get; set; }
        public string Picture { get; set; }
        public Donor Donor { get; set; } 
    }
}
