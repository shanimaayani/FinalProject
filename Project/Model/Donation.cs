using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public int PresentId { get; set; }
        public string DonorId { get; set; }


        public Present Present { get; set; }
        public Donor Donor { get; set; }

    }
}
