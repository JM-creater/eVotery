using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.TotalFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class TotalController : ControllerBase
{
    private readonly ITotalService service;
    public TotalController(ITotalService _service)
    {
        service = _service;
    }

    [HttpGet("total-candidates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCandidates()
    {
        var response = service.GetTotalCandidates();

        return Ok(response);
    }

    [HttpGet("total-positions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPositions()
    {
        var response = service.GetTotalPosition();

        return Ok(response);
    }

    [HttpGet("total-voters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetVoters()
    {
        var response = service.GetTotalVoters();

        return Ok(response);
    }

    [HttpGet("total-votes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetVotes()
    {
        var response = service.GetTotalVotes();

        return Ok(response);
    }
}
