using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVotesService service;

    public VotesController(IVotesService _service)
    {
        service = _service;
    }

    [HttpPost("submit-vote")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitVote([FromBody] SubmitVoteDto dto)
    {
        var response = await service.SubmitVote(dto);

        return Ok(response);
    }
}
