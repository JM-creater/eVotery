using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.IServices;

public interface IElectionService
{
    Task<ApiResponse<Election>> Create(CreateElectionDto dto);
    Task<List<Election>> GetAll();
    Task<Election> GetById(Guid id);
    Task<ApiResponse<Election>> Update(Guid id, UpdateElectionDto dto);
    Task<ApiResponse<Election>> Delete(Guid id);
}
