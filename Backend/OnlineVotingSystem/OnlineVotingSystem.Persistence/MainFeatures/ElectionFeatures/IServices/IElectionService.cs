using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.IServices;

public interface IElectionService
{
    Task<ApiResponse> Create(CreateElectionDto dto);
    Task<List<Election>> GetAll();
    Task<Election> GetById(Guid id);
}
