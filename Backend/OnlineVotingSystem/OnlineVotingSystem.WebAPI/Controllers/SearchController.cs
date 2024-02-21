using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

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
}
