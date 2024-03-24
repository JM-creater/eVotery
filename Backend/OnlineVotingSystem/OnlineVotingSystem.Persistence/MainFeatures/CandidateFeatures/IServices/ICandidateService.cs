using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

public interface ICandidateService
{
    Task<ApiResponse<Candidate>> Create(CreateCandidateDto dto);
    Task<List<Candidate>> GetAll();
    Task<Candidate> GetById(Guid id);
    Task<ApiResponse<Candidate>> Update(Guid id, UpdateCandidateDto dto);
    Task<ApiResponse<Candidate>> Delete(Guid id);
    Task<ApiResponse<Candidate>> ActivateCandidate(Guid id);
    Task<ApiResponse<Candidate>> DeactivateCandidate(Guid id);
}
