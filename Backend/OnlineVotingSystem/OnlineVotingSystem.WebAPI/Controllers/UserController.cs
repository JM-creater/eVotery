using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    public UserController(IUserService _service)
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

    [HttpGet("get-by-id/{id}")]
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

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVoterDto dto)
    {
        var response = await service.Login(dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var response = await service.ForgotPassword(email);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var response = await service.ResetPassword(dto);

        if (response == null)
        {
            return NotFound();
        }

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
