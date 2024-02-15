using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class VoterController : ControllerBase
{
    private readonly IVoterService service;
    public VoterController(IVoterService _service)
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
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Register([FromForm] CreateVoterDto dto)
    {
        var response = await service.Register(dto);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVoterDto dto)
    {
        var response = await service.Login(dto);

        return Ok(response);
    }

    [HttpPut("validate/{id}")]
    public async Task<IActionResult> Validate([FromRoute] int id)
    {
        var response = await service.Validate(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("deactivated/{id}")]
    public async Task<IActionResult> Deactivated([FromRoute] int id)
    {
        var response = await service.Deactivated(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("activated/{id}")]
    public async Task<IActionResult> Activated([FromRoute] int id)
    {
        var response = await service.Activated(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

}
