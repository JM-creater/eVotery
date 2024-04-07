using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.Services;

public class VotesService : IVotesService
{
    private readonly DataContext context;
    public VotesService(DataContext _context)
    {
        context = _context;
    }


}
