using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.ResultFeatures.IServices;

public interface IResultService
{
    Task<ApiResponse<ElectionResultDto>> GetElectionResultsByPosition(Guid positionId);
}
