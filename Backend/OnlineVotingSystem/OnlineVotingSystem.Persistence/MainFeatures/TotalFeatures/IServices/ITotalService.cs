namespace OnlineVotingSystem.Persistence.MainFeatures.TotalFeatures.IServices;

public interface ITotalService
{
    int GetTotalPosition();
    int GetTotalCandidates();
    int GetTotalVoters();
    int GetTotalVotes();
}
