using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateService servvice;
    public CandidateController(ICandidateService _servvice)
    {
        servvice = _servvice;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateCandidateDto dto)
    {
        var response = await servvice.Create(dto);

        return Ok(response);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await servvice.GetAll();

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await servvice.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
