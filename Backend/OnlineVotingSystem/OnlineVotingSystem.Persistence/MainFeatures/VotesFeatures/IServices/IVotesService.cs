using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

public interface IVotesService
{
    Task<ApiResponse<Vote>> SubmitVote(SubmitVoteDto dto);
}
