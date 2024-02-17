using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

public class PositionController : ControllerBase
{
    private readonly IPositionService service;
    public PositionController(IPositionService _service)
    {
        service = _service;
    }

    [HttpGet("get-all")]
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
