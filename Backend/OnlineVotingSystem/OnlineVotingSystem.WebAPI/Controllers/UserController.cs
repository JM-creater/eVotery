using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;
using System.Net;

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

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        var json = JsonConvert.SerializeObject(response, settings);

        return new ContentResult
        {
            Content = json,
            ContentType = "application/json",
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    [HttpGet("get-by-id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await service.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("validate-reset-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateResetToken([FromQuery] string token)
    {
        var isValid = await service.IsResetTokenValid(token);

        return Ok(new { isValid });
    }

    [AllowAnonymous]
    [HttpPost("register-first-step")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StepOneRegister([FromBody] StepOneRegisterDto dto)
    {
        var response = await service.StepOneRegister(dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromForm] CreateVoterDto dto)
    {
        var response = await service.Register(dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginVoterDto dto)
    {
        var response = await service.Login(dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPut("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
