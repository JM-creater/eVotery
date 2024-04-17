using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.TotalFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.TotalFeatures.Services;

public class TotalService : ITotalService
{
    private readonly DataContext context;
    public TotalService(DataContext _context)
    {
        context = _context;
    }

    public int GetTotalCandidates()
     => context.Candidates.Count();

    public int GetTotalPosition()
     => context.Positions.Count();

    public int GetTotalVoters()
      => context.Users.Count(u => u.Role == Domain.Enum.UserRole.Voter);

    public int GetTotalVotes()
      => context.Votes.Count();
}
