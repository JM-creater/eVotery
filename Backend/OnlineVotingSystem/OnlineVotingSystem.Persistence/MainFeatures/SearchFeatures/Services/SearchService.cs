using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.Services;

public class SearchService : ISearchService
{
    private readonly DataContext context;
    public SearchService(DataContext _context)
    {
        context = _context;
    }

    public async Task<User> SearchQuery(string searchQuery)
    {
        var search = await context.Users
                                  .Where(u => u.FirstName.Contains(searchQuery.ToLower()) ||
                                              u.LastName.Contains(searchQuery.ToLower()))
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("User not found with the given search query.");
        }

        return search;
    }

    public async Task<Position> SearchPositionName(string searchQuery)
    {
        var search = await context.Positions
                                  .Where(u => u.Name.Contains(searchQuery.ToLower()))
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("Position not found with the given search query.");
        }

        return search;
    }

    public async Task<Ballot> SearchBallotName(string searchQuery)
    {
        var search = await context.Ballots
                                  .Where(b => b.BallotName.Contains(searchQuery.ToLower()))
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("Ballot not found with the given search query.");
        }

        return search;
    }

    public async Task<Election> SearchElectionName(string searchQuery)
    {
        var search = await context.Elections
                                  .Where(b => b.ElectionName.Contains(searchQuery.ToLower()))
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("Election not found with the given search query.");
        }

        return search;
    }

    public async Task<PartyAffiliation> SearchParty(string searchQuery)
    {
        var search = await context.PartyAffiliations
                                  .Where(pa => pa.PartyName.Contains(searchQuery.ToLower()))    
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("Party not found with the given search query.");
        }

        return search;
    }

    public async Task<Candidate> SearchCandidate(string searchQuery)
    {
        var search = await context.Candidates
                                  .Where(c => c.FirstName.Contains(searchQuery.ToLower()) || 
                                              c.LastName.Contains(searchQuery.ToLower()))
                                  .FirstOrDefaultAsync();

        if (search == null)
        {
            throw new InvalidOperationException("Candidate not found with the given search query.");
        }

        return search;
    }
}
