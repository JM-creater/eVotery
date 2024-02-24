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

}
