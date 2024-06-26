﻿using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateService servvice;
    public CandidateController(ICandidateService _servvice)
    {
        servvice = _servvice;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateCandidateDto dto)
    {
        var response = await servvice.Create(dto);

        return Ok(response);
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var response = await servvice.GetAll();

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await servvice.GetById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("count-votes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCountVotesByUser(Guid id)
    {
        var response = await servvice.GetCountVotes(id);

        return Ok(response);
    }

    [HttpPut("update-candidate/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCandidate([FromRoute] Guid id, [FromForm] UpdateCandidateDto dto)
    {
        var response = await servvice.Update(id, dto);

        return Ok(response);
    }

    [HttpPut("activate-candidate/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ActivateCandidate(Guid id)
    {
        var response = await servvice.ActivateCandidate(id);

        return Ok(response);
    }

    [HttpPut("deactivate-candidate/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeactivateCandidate(Guid id)
    {
        var response = await servvice.DeactivateCandidate(id);

        return Ok(response);
    }

    [HttpDelete("delete-candidate/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCandidate([FromRoute] Guid id)
    {
        var response = await servvice.Delete(id);

        return Ok(response);
    }
}
