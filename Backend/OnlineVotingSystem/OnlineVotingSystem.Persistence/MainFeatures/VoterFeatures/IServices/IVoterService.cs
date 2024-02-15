using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

public interface IVoterService
{
    Task<List<Voter>> GetAll();
    Task<Voter> GetById(int id);
    Task<ApiResponse> Register(CreateVoterDto dto);
    Task<ApiResponse> Login(LoginVoterDto dto);
    Task<ApiResponse> Validate(int id);
    Task<ApiResponse> Deactivated(int id);
    Task<ApiResponse> Activated(int id);
    Task<ApiResponse> UpdateVoterProfile(int id, UpdateVoterDto dto);
}
