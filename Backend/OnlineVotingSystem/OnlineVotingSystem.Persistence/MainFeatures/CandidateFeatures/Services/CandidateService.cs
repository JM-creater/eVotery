using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.Services;

public class CandidateService : ICandidateService
{
    private readonly DataContext context;
    public CandidateService(DataContext _context)
    {
        context = _context;
    }
}
