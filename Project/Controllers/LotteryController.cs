using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.DTO;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Authorize(Roles = "True")]
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;
        private readonly IMapper _mapper;

        public LotteryController(ILotteryService lotteryService, IMapper mapper)
        {
            _lotteryService = lotteryService;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<Lottery>> Get()
        {
            return await _lotteryService.GetPresentsAsync();
            //return _mapper.Map<List<LotteryDto>>(lottries);
 
        }


        // POST api/<ValuesController>
        
        [HttpPost]
        public async Task<UserDto> Post([FromBody] PresentDto present)
        {
            var _present = _mapper.Map<Present>(present);              
            var user= await _lotteryService.LotteryUser(_present);
            return _mapper.Map<UserDto>(user);
        }

    }
}
