using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class BallotController : ControllerBase
{
    private readonly IBallotService service;
    public BallotController(IBallotService _service)
    {
        service = _service;
    }

    [HttpPost("create-ballot")]
    public async Task<IActionResult> Create([FromBody] CreateBallotDto dto)
    {
        var response = await service.Create(dto);

        return Ok(response);
    }

    [HttpGet("getall-ballots")]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAll();

        if (response == null) 
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("getById-ballot/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("update-ballot/{id}")]
    public async Task<IActionResult> UpdateBallot([FromRoute] Guid id, [FromBody] UpdateBallotDto dto)
    {
        var response = await service.Update(id, dto);

        return Ok(response);
    }

    [HttpDelete("delete-ballot/{id}")]
    public async Task<IActionResult> DeleteBallot([FromRoute] Guid id)
    {
        var response = await service.Delete(id);

        return Ok(response);
    }
}
