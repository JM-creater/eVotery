using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;

public interface ISearchService
{
    Task<User> SearchQuery(string searchQuery);
}
