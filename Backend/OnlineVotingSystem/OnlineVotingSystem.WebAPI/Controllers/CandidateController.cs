using Microsoft.AspNetCore.Mvc;
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
}
