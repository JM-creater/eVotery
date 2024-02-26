using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

namespace OnlineVotingSystem.WebAPI.Controllers;

public class PartyAffiliationController : ControllerBase
{
    private readonly IPartyAffiliationService service;
    public PartyAffiliationController(IPartyAffiliationService _service)
    {
        service = _service;
    }
}
