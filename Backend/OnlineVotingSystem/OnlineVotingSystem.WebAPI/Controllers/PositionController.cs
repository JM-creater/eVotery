using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class PositionController : ControllerBase
{
    private readonly IPositionService service;
    public PositionController(IPositionService _service)
    {
        service = _service;
    }

    [HttpPost("create-position")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(string name)
    {
        var response = await service.Create(name);

        return Ok(response);
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAll();

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("get-count-by-name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Dictionary<string, int>>> GetCountCandidates()
    {
        var response = await service.GetNumberOfCandidatePosition();

        return Ok(response);
    }

    [HttpPut("update-position/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePositionDto dto)
    {
        var response = await service.Update(id, dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpDelete("delete-position/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await service.Delete(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
