using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVotesService service;

    public VotesController(IVotesService _service)
    {
        service = _service;
    }


}
