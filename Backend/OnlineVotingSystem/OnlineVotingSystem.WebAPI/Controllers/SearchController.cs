using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService service;
    public SearchController(ISearchService _service)
    {
        service = _service;
    }

    [HttpGet("search-voter")]
    public async Task<IActionResult> SearchQuery(string searchQuery)
    {
        var response = await service.SearchQuery(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("search-position")]
    public async Task<IActionResult> SearchPosition(string searchQuery)
    {
        var response = await service.SearchPositionName(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
