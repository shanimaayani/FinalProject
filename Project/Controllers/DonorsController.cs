using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.DTO;
using Project.Model;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donorService;
        private readonly IMapper _mapper;

        public DonorsController(IDonorService donorService,IMapper mapper)
        {
            _donorService = donorService;
            _mapper = mapper;
        }

        // GET: api/<DonorsController>
        [HttpGet]
        public async Task<List<DonorDto>> Get()
        {
            var donors =  await _donorService.GetDonorsAsync();
            return _mapper.Map<List<DonorDto>>(donors);
            
        }

        // GET: api/<DonorsController>
        [Authorize(Roles = "True")]
        [HttpGet]
        [Route("GetMyPresents")]
        public async Task<List<PresentDto>> GetMyPresents(string id)
        {
            var present = await _donorService.GetAllPresentsByDonorIdAsync(id);
            return _mapper.Map<List<PresentDto>>(present);
  
        }

        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public async Task<DonorDto> Get(string id)
        {
            var _donor = await _donorService.GetDonorAsync(id);
            return _mapper.Map<DonorDto>(_donor);
        }
        [Authorize(Roles = "True")]
        [HttpGet]
        [Route("filterDonor")]
        public async Task<List<DonorDto>> Get(string? firstName, string? lastName, string? email, string? present)
        {
            var _donors = await _donorService.FilterDonor(firstName, lastName, email, present);
            return _mapper.Map<List<DonorDto>>(_donors);

        }
       [Authorize(Roles = "True")]
        // POST api/<PresentsController>
        [HttpPost]
        public async Task<string> Post([FromBody] DonorDto donor)
        {
            var _donor = _mapper.Map<Donor>(donor);
            if (ModelState.IsValid)
            {
                return await _donorService.AddDonorAsync(_donor);
            }
            return "";
            
        }
        [Authorize(Roles = "True")]
        // PUT api/<PresentsController>/5
        [HttpPut]
        public async Task<DonorDto> Put([FromBody] DonorDto donor)
        {
            var _donor = _mapper.Map<Donor>(donor);
            if (ModelState.IsValid)
            {
                var dono= await _donorService.UpdateDonorAsync(_donor);
                return _mapper.Map<DonorDto>(dono);
            }
            return null;
            
        }
        [Authorize(Roles = "True")]
        // DELETE api/<PresentsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _donorService.DeleteDonorAsync(id);
        }


        
    }
}
