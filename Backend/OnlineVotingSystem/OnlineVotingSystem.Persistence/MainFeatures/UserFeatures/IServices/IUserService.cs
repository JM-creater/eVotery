using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

public interface IUserService
{
    Task<IEnumerable<GetAllUserDto>> GetAll();
    Task<User> GetById(int id);
    Task<ApiResponse<User>> StepOneRegister(StepOneRegisterDto dto);
    Task<ApiResponse<User>> StepTwoRegister(Guid id, StepTwoRegisterDto dto);
    Task<ApiResponse<User>> StepThreeRegister(Guid id, StepThreeRegisterDto dto);
    Task<ApiResponse<User>> SubStepThreeRegister(Guid id, SubStepThreeRegisterDto dto);
    Task<ApiResponse<User>> Login(LoginVoterDto dto);
    Task<bool> GetreCaptchaResponse(string userResponse);
    Task<ApiResponse<User>> Validate(int id);
    Task<ApiResponse<User>> Deactivated(int id);
    Task<ApiResponse<User>> Activated(int id);
    Task<ApiResponse<User>> UpdateVoterProfile(int id, UpdateVoterDto dto);
    Task<ApiResponse<User>> ForgotPassword(string email);
    Task<ApiResponse<User>> ResetPassword(ResetPasswordDto dto);
    Task<bool> IsResetTokenValid(string token);
    Task<ApiResponse<User>> Delete(Guid id);
    Task<ApiResponse<User>> RememberMe(Guid id);
}
