using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.IServices;

public interface IBallotService
{
    Task<ApiResponse> Create(CreateBallotDto dto);
    Task<List<Ballot>> GetAll();
    Task<Ballot> GetById(Guid id);
}
