using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentController : ControllerBase
    {

        private readonly IPresentService _presentService;

        public PresentController(IPresentService presentService)
        {
            _presentService = presentService;
        }


        [HttpGet]
        [Route("GetPresents")]
        public Task<List<Present>> GetPresents()
        {
            return _presentService.GetPresentsAsync();
        }

        // GET api/<PresentController>/5
        [HttpGet]
        [Route("GetPresent")]
        public Task<Present> GetPresent(int id)
        {
            return _presentService.GetPresentAsync(id);
        }

        // POST api/<PresentController>
        [HttpPost]
        [Route("AddPresent")]
        public Task<int> AddPresent(Present present)
        {
            return _presentService.AddPresent(present);
        }



        // PUT api/<PresentController>/5
        [HttpPut]
        [Route("UpdatePresent")]
        public Task<Present> UpdatePresent(Present present)
        {
            return _presentService.UpdatePresent(present);

        }

        // DELETE api/<PresentController>/5
        [HttpDelete]
        [Route("DeletePresent")]
        public Task<bool> DeletePresent(int id)
        {
            return _presentService.DeletePresent(id);
        }
    }
}
