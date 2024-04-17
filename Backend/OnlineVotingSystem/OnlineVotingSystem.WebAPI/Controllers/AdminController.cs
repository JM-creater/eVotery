using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService service;
    public AdminController(IAdminService _service)
    {
        service = _service;
    }

    [HttpGet("admin-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [EnableRateLimiting("fixed")]
    public async Task<IActionResult> GetAccountInfo()
    {
        var response = await service.GetAdminAccountInfo();

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
