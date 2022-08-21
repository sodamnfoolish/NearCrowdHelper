using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoonStoneController : ControllerBase
    {
        private readonly MoonStoneService _moonStoneService;

        public MoonStoneController(IConfiguration configuration)
        {
            _moonStoneService = new(configuration);
        }

        [HttpPost("FindHack")]
        public async Task<IEnumerable<string>> Get([FromBody] FindHackDTO findHackDTO)
            => await _moonStoneService.FindHack(findHackDTO.contest, findHackDTO.problem, findHackDTO.test);
    }
}