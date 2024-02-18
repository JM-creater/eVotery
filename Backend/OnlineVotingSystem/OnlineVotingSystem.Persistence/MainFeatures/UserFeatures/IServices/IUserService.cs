using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task<ApiResponse> Register(CreateVoterDto dto);
    Task<ApiResponse> Login(LoginVoterDto dto);
    Task<ApiResponse> Validate(int id);
    Task<ApiResponse> Deactivated(int id);
    Task<ApiResponse> Activated(int id);
    Task<ApiResponse> UpdateVoterProfile(int id, UpdateVoterDto dto);
    Task<ApiResponse> ForgotPassword(string email);
    Task<ApiResponse> ResetPassword(ResetPasswordDto dto);
}
