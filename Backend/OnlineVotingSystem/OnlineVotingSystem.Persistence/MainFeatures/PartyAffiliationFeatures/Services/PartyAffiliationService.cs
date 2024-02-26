using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.Services;

public class PartyAffiliationService : IPartyAffiliationService
{
    private readonly DataContext context;
    public PartyAffiliationService(DataContext _context)
    {
        context = _context;
    }
}
