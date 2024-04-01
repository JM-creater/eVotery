using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.Helpers.CaptchaResponse;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;
using System.Net;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public UserController(IUserService _service, IConfiguration configuration, HttpClient httpClient)
    {
        service = _service;
        _configuration = configuration;
        _httpClient = httpClient;
    }

    [HttpGet("Captcha")]
    public async Task<bool> GetreCaptchaResponse(string userResponse)
    {
        var reCaptchaSecretKey = _configuration["reCaptcha:SecretKey"];

        if (reCaptchaSecretKey != null && userResponse != null)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"secret", reCaptchaSecretKey },
                    {"response", userResponse }
                });
            var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<reCaptchaResponse>();
                return result.Success;
            }
        }
        return false;
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
    [HttpPut("register-second-step/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StepTwoRegister([FromRoute] Guid id, [FromBody] StepTwoRegisterDto dto)
    {
        var response = await service.StepTwoRegister(id, dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPut("register-third-step/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StepThreeRegister([FromRoute] Guid id, [FromBody] StepThreeRegisterDto dto)
    {
        var response = await service.StepThreeRegister(id, dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPut("register-subthird-step/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubStepThreeRegister([FromRoute] Guid id, [FromForm] SubStepThreeRegisterDto dto)
    {
        var response = await service.SubStepThreeRegister(id, dto);

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
