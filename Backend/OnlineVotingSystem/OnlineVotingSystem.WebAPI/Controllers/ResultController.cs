using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.ResultFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class ResultController : ControllerBase
{
    private readonly IResultService service;
    public ResultController(IResultService _service)
    {
        service = _service;
    }

    [HttpGet("results/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetResultsByPosition(Guid id)
    {
        var response = await service.GetElectionResultsByPosition(id);

        if (response == null)
        {
            return NotFound("No results found for the given position.");
        }

        return Ok(response);
    }
}
