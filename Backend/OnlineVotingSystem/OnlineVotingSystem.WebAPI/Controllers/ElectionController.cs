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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateElectionDto dto)
    {
        var response = await service.Create(dto);

        return Ok(response);
    }

    [HttpGet("getall-elections")]
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("update-election/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateElection(Guid id, UpdateElectionDto dto)
    {
        var response = await service.Update(id, dto);
    
        return Ok(response);
    }

    [HttpDelete("delete-election/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteElection(Guid id)
    {
        var response = await service.Delete(id);

        return Ok(response);
    }
}
