using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class ElectionController : ControllerBase
{
    private readonly IElectionService service;
    public ElectionController(IElectionService _service)
    {
        service = _service;
    }

    [HttpPost("create-election")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateElectionDto dto)
    {
        var response = await service.Create(dto);

        return Ok(response);
    }

    [HttpGet("getall-elections")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAll();

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("getById-election/{id}")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("update-election/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateElection([FromRoute] Guid id, [FromBody] UpdateElectionDto dto)
    {
        var response = await service.Update(id, dto);
    
        return Ok(response);
    }

    [HttpDelete("delete-election/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteElection([FromRoute] Guid id)
    {
        var response = await service.Delete(id);

        return Ok(response);
    }
}
