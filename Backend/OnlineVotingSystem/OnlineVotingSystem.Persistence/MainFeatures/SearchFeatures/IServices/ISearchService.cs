using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;

public interface ISearchService
{
    Task<User> SearchQuery(string searchQuery);
    Task<Position> SearchPositionName(string searchQuery);
    Task<Ballot> SearchBallotName(string searchQuery);
    Task<Election> SearchElectionName(string searchQuery);
    Task<PartyAffiliation> SearchParty(string searchQuery);
    Task<Candidate> SearchCandidate(string searchQuery);
}
