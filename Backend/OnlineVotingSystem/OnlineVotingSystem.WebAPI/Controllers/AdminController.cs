using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("GetUsersForAdmin/{voterId}")]
    public async Task<IActionResult> GetUsersForAdmin(int voterId)
    {
        var response = await service.GetUsersForAdmin(voterId);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
