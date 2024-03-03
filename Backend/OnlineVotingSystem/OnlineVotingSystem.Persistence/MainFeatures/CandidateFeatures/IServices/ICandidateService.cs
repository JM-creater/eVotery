using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

public interface ICandidateService
{
    Task<ApiResponse> Create(CreateCandidateDto dto);
    Task<List<Candidate>> GetAll();
    Task<Candidate> GetById(Guid id);
    Task<ApiResponse> Update(UpdateCandidateDto dto);
}
