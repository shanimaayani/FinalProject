using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }
       

        [HttpGet]
        [Route("GetDonors")]
        public Task<List<Donor>> GetDonors()
        {
            return _donorService.GetDonorsAsync();
        }

        // GET api/<DonorController>/5
        [HttpGet]
        [Route("GetDonor")]
        public Task<Donor> GetDonor(string id)
        {
            return _donorService.GetDonorAsync(id);
        }

        // POST api/<DonorController>
        [HttpPost]
        [Route("AddDonor")]
        public Task<string> AddDonor(Donor donor)
        {
            return _donorService.AddDonor(donor);
        }



        // PUT api/<DonorController>/5
        [HttpPut]
        [Route("UpdateDonor")]
        public Task<Donor> UpdateDonor(Donor donor)
        {
            return _donorService.UpdateDonor(donor);

        }

        // DELETE api/<DonorController>/5
        [HttpDelete]
        [Route("DeleteDonor")]
        public Task<bool> DeleteDonor(string id)
        {
            return _donorService.DeleteDonor(id);
        }

    }
}
