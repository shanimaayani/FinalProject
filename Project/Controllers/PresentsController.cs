using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.BLL;
using Project.DTO;
using Project.Model;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PresentsController : ControllerBase
    {
        private readonly IPresentService _presentService;
        private readonly IMapper _mapper;

        public PresentsController(IPresentService presentService,IMapper mapper)
        {
            _presentService = presentService;
            _mapper = mapper;
        }

        // GET: api/<PresentsController>
        [HttpGet]
        public async Task<List<PresentDto>> Get()//List<PresentDto>
        {
            var presents = await _presentService.GetPresentsAsync();
            var _presents = _mapper.Map<List<PresentDto>>(presents);
            return _presents;
        }

        [HttpGet]
        [Route("filterPresent")]
        public async Task<List<PresentDto>> Get( string? presentName, string? presentDonor, string? presentCategory, int? numOfPurch)
        {
            var presents= await _presentService.FilterPresent(presentName, presentDonor,  presentCategory, numOfPurch);
            return _mapper.Map<List<PresentDto>>(presents);
        }
        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public async  Task<PresentDto> Get(int id)
        {
            var present = await _presentService.GetPresentAsync(id);
            return _mapper.Map<PresentDto>(present);
        }

        [Authorize(Roles = "True")]
        // POST api/<PresentsController>
        [HttpPost]
        public async Task<int> Post([FromBody] PresentDto present)
        {
            
            var _present = _mapper.Map<Present>(present);
            if (ModelState.IsValid)
            {
                return await _presentService.AddPresntAsync(_present);
            }
            return -1;
            
        }

        [Authorize(Roles = "True")]
        // PUT api/<PresentsController>/5
        [HttpPut]
        public async Task<PresentDto> Put( [FromBody] PresentDto present)
        {
                var _present = _mapper.Map<Present>(present);
            if (ModelState.IsValid)
            {
                var pres= await _presentService.UpdatePresentAsync(_present);
                return _mapper.Map<PresentDto>(pres);
            }
            return null;
        }
        [Authorize(Roles = "True")]
        // DELETE api/<PresentsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _presentService.DeletePresntAsync(id);
        }

        [Authorize(Roles = "True")]
        [HttpDelete]
        public async Task<bool> DeletePresents([FromQuery] int[] presents)
        {
            return await _presentService.DeletePresntsAsync(presents);
        }

    }
}
