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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchPosition(string searchQuery)
    {
        var response = await service.SearchPositionName(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("search-ballot")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchBallot(string searchQuery)
    {
        var response = await service.SearchBallotName(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("search-election")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchElection(string searchQuery)
    {
        var response = await service.SearchElectionName(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("search-party")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchParty(string searchQuery)
    {
        var response = await service.SearchParty(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("search-candidate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchCandidates(string searchQuery)
    {
        var response = await service.SearchCandidate(searchQuery);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
