using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class PersonalDocumentController : ControllerBase
{
    private readonly IPersonalDocumentsService service;
    public PersonalDocumentController(IPersonalDocumentsService _service)
    {
        service = _service;
    }

    [HttpGet("getall-documents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllDocuments()
    {
        var response = await service.GetAll();

        return Ok(response);
    }
}
