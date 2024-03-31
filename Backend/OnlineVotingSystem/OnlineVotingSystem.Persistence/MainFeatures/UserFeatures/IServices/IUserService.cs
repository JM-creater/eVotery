using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

public interface IUserService
{
    Task<IEnumerable<GetAllUserDto>> GetAll();
    Task<User> GetById(int id);
    Task<ApiResponse<User>> Register(CreateVoterDto dto);
    Task<ApiResponse<User>> StepOneRegister(StepOneRegisterDto dto);
    Task<ApiResponse<User>> Login(LoginVoterDto dto);
    Task<ApiResponse<User>> Validate(int id);
    Task<ApiResponse<User>> Deactivated(int id);
    Task<ApiResponse<User>> Activated(int id);
    Task<ApiResponse<User>> UpdateVoterProfile(int id, UpdateVoterDto dto);
    Task<ApiResponse<User>> ForgotPassword(string email);
    Task<ApiResponse<User>> ResetPassword(ResetPasswordDto dto);
    Task<bool> IsResetTokenValid(string token);
}
