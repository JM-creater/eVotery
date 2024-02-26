using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class PartyAffiliationController : ControllerBase
{
    private readonly IPartyAffiliationService service;
    public PartyAffiliationController(IPartyAffiliationService _service)
    {
        service = _service;
    }

    [HttpPost("create-party")]
    public async Task<IActionResult> Create([FromForm] CreatePartyAffiliationDto dto)
    {
        var response = await service.Create(dto);

        return Ok(response);
    }

    [HttpGet("getall-party")]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAll();

        return Ok(response);
    }

    [HttpGet("getbyid-party/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await service.GetById(id);

        return Ok(response);
    }
}
