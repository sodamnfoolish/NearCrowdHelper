using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Domain.DTOs;
using Domain.Extensions;

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

        [HttpPost("FindHandlesByHack")]
        public async Task<ActionResult<IEnumerable<string>>> FindHandles([FromBody] FindHandlesByHackDTO findHandlesByHackDTO)
            => Ok(await _moonStoneService.FindHandlesByHack(
                findHandlesByHackDTO.problem.GetContestId(), 
                findHandlesByHackDTO.problem.GetIndex(), 
                findHandlesByHackDTO.input.ReplaceLineEndings(), 
                findHandlesByHackDTO.output!.ReplaceLineEndings()));

        [HttpPost("FindOkSubmissionsByHack")]
        public async Task<ActionResult<IEnumerable<int>>> FindOkSubmissionsByHack([FromBody] FindOkSubmissionsByHackDTO findOkSubmissionsByHackDTO)
            => Ok(await _moonStoneService.FindOkSubmissionsByHack(
                findOkSubmissionsByHackDTO.problem.GetContestId(),
                findOkSubmissionsByHackDTO.problem.GetIndex(),
                findOkSubmissionsByHackDTO.input.ReplaceLineEndings(),
                findOkSubmissionsByHackDTO.output!.ReplaceLineEndings()));
    }
}