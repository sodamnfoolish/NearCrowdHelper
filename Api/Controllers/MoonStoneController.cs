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
        public async Task<ActionResult<IEnumerable<string>>> Get([FromBody] FindHackDTO findHackDTO)
        {
            try
            {
                return Ok(await _moonStoneService.FindHack(findHackDTO.problem, findHackDTO.input, findHackDTO.output));
            }
            catch (Exception ex)
            {
                return StatusCode(424, ex.Message);
            }
        }
    }
}