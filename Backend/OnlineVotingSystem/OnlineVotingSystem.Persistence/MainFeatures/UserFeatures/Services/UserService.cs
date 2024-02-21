using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineVotingSystem.Application.ImageDirectory;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Enum;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.Helpers.EmailContent;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.Helpers.Security;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.Services;

public class UserService : IUserService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    private readonly ILogger<UserService> logger;
    private readonly IConfiguration configuration;
    public UserService(DataContext _context, IMapper _mapper, ILogger<UserService> _logger, IConfiguration _configuration)
    {
        context = _context;
        mapper = _mapper;
        logger = _logger;
        configuration = _configuration;
    }

    public async Task<List<User>> GetAll()
     => await context.Users
                     .Where(u => u.Role == UserRole.Voter)
                     .OrderByDescending(v => v.DateCreated)
                     .ToListAsync();

    public async Task<User> GetById(int id)
     => await context.Users
                     .Where(v => v.VoterId.Equals(id))
                     .FirstOrDefaultAsync();

    public async Task<ApiResponse> Register(CreateVoterDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User existingFirstName = await context.Users
                                                  .Where(v => v.FirstName.Equals(dto.FirstName))
                                                  .FirstOrDefaultAsync();

            if (existingFirstName != null)
            {
                string errorMessage = $"A voter with the first name '{dto.FirstName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            User existingLastName = await context.Users
                                                  .Where(v => v.LastName.Equals(dto.LastName))
                                                  .FirstOrDefaultAsync();

            if (existingLastName != null)
            {
                string errorMessage = $"A voter with the last name '{dto.LastName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            User existingEmail = await context.Users
                                               .Where(v => v.Email.Equals(dto.Email))
                                               .FirstOrDefaultAsync();

            if (existingEmail != null)
            {
                string errorMessage = $"A voter with email '{dto.Email}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            User existingPhoneNumber = await context.Users
                                                     .Where(v => v.PhoneNumber.Equals(dto.PhoneNumber))
                                                     .FirstOrDefaultAsync();

            if (existingPhoneNumber != null)
            {
                string errorMessage = $"A voter with phone number '{dto.PhoneNumber}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var voterImagePath = await new ImagePathConfig().SaveVoterImages(dto.VoterImages);
            var generateVoterID = Tokens.GenerateVoterID();
            var passwordEncrypt = PasswordHasher.EncryptPassword(dto.Password);

            var student = mapper.Map<User>(dto);
            student.VoterImages = voterImagePath;
            student.VoterId = generateVoterID;
            student.Password = passwordEncrypt;
            student.IsValidate = false;
            student.IsActive = true;
            student.DateCreated = DateTime.Now;
            student.VerificationStatus = VerifyStatus.Pending;
            student.Role = UserRole.Voter;

            context.Users.Add(student);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
            response.UserRole = UserRole.Voter;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> Login(LoginVoterDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User voter = null;

            if (dto.VoterId.HasValue)
            {
                voter = await context.Users
                                     .Where(v => v.VoterId.Equals(dto.VoterId.Value))
                                     .FirstOrDefaultAsync();
            }
            else if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                voter = await context.Users
                                     .Where(v => v.Email.Equals(dto.Email))
                                     .FirstOrDefaultAsync();
            }
            else
            {
                string errorMessage = "No valid identifier provided.";
                response.ErrorMessage = errorMessage;
                throw new ArgumentException(errorMessage);
            }

            if (voter == null)
            {
                string errorMessage = "Voter not yet registered.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            if (!voter.IsValidate)
            {
                string errorMessage = "Waiting for validation";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            if (!voter.IsActive)
            {
                string errorMessage = "Account is deactivated";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            if (!PasswordHasher.VerifyPassword(dto.Password, voter.Password))
            {
                string errorMessage = "Incorrect Password";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            response.ResponseCode = 200;
            response.UserRole = voter.Role;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }


    public async Task<ApiResponse> Validate(int id)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User voter = await context.Users
                                       .Where(v => v.VoterId.Equals(id))
                                       .FirstOrDefaultAsync();

            if (voter == null)
            {
                string errorMessage = "Voter's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            voter.IsValidate = true;

            context.Users.Update(voter);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> Deactivated(int id)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User voter = await context.Users
                                       .Where(v => v.VoterId.Equals(id))
                                       .FirstOrDefaultAsync();

            if (voter == null)
            {
                string errorMessage = "Voter's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            voter.IsActive = false;

            context.Users.Update(voter);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> Activated(int id)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User voter = await context.Users
                                       .Where(v => v.VoterId.Equals(id))
                                       .FirstOrDefaultAsync();

            if (voter == null)
            {
                string errorMessage = "Voter's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            voter.IsActive = true;

            context.Users.Update(voter);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> UpdateVoterProfile(int id, UpdateVoterDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User voter = await context.Users
                                       .Where(v => v.VoterId.Equals(id))
                                       .FirstOrDefaultAsync();

            if (voter == null)
            {
                string errorMessage = "Voter's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            if (dto.VoterImages != null)
            {
                voter.VoterImages = await new ImagePathConfig().SaveVoterImages(dto.VoterImages);
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                voter.Password = PasswordHasher.EncryptPassword(dto.Password);
            }

            var updatedVoterProfile = mapper.Map(dto, voter);
            context.Users.Update(updatedVoterProfile);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;

        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> ForgotPassword(string email)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User user = await context.Users
                                     .Where(u => u.Email == email)
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "Email not yet registered.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            user.PasswordResetToken = Tokens.GenerateToken(user, configuration);
            user.ResetTokenExpires = DateTime.Now.AddHours(24);

            context.Users.Update(user);
            await context.SaveChangesAsync();

            var emailProvider = new EmailContentProvider(configuration);
            await emailProvider.SendPasswordResetEmail(user.Email, user.PasswordResetToken);

            response.ResponseCode = 200;

        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse> ResetPassword(ResetPasswordDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            User user = await context.Users
                                     .Where(u => u.PasswordResetToken == dto.Token) 
                                     .FirstOrDefaultAsync();

            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                string errorMessage = "Invalid Token.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            user.Password = PasswordHasher.EncryptPassword(dto.NewPassword);  
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            context.Users.Update(user);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }


    public async Task<bool> IsResetTokenValid(string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var user = await context.Users
                                    .Where(u => u.PasswordResetToken == token
                                            && u.ResetTokenExpires > DateTime.UtcNow)
                                    .FirstOrDefaultAsync();

            return user != null;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}
