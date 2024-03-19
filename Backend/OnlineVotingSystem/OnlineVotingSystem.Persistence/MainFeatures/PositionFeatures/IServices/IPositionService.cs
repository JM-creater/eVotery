using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

public interface IPositionService
{
    Task<List<Position>> GetAll();
    Task<Position> GetById(Guid id);
    Task<Dictionary<string, int>> GetNumberOfCandidatePosition();
    Task<ApiResponse<Position>> Create(string name);
    Task<ApiResponse<Position>> Update(Guid id, UpdatePositionDto dto);
    Task<ApiResponse<Position>> Delete(Guid id);
}
