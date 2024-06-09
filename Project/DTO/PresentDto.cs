using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.DTO
{
    public class PresentDto
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [DefaultValue(10)]
        [Range(10, 60)]
        public int Price { get; set; }
        public string Category { get; set; }
        public string Picture { get; set; }
        public string DonorId { get; set; }
    }
}
