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
using System.Net.Http;
using System.Net.Http.Json;

namespace OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.Services;

public class UserService : IUserService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    private readonly ILogger<UserService> logger;
    private readonly IConfiguration configuration;
    private readonly HttpClient _httpClient;
    public UserService(DataContext _context, IMapper _mapper, ILogger<UserService> _logger, IConfiguration _configuration, HttpClient httpClient)
    {
        context = _context;
        mapper = _mapper;
        logger = _logger;
        configuration = _configuration;
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetAll()
    {
        var student = await context.Users
                                   .Where(u => u.Role == UserRole.Voter)
                                   .AsNoTracking()
                                   .OrderByDescending(v => v.DateCreated)
                                   .ToListAsync();

        return student;
    }

    public async Task<User> GetById(int id)
     => await context.Users
                     .Where(v => v.VoterId.Equals(id))
                     .FirstOrDefaultAsync();

    public async Task<ApiResponse<User>> StepOneRegister(StepOneRegisterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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

            var generateVoterID = Tokens.GenerateVoterID();

            var student = mapper.Map<User>(dto);
            student.VoterId = generateVoterID;
            student.Role = UserRole.Voter;

            context.Users.Add(student);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
            response.Result = student;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> StepTwoRegister(Guid id, StepTwoRegisterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

        try
        {
            User user = await context.Users
                                     .Where(u => u.Id.Equals(id))
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "User's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            User existingPhoneNumber = await context.Users
                                                    .Where(u => u.PhoneNumber.Equals(dto.PhoneNumber))
                                                    .FirstOrDefaultAsync();

            if (existingPhoneNumber != null)
            {
                string errorMessage = $"A voter with the phone number '{dto.PhoneNumber}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            User existingEmail = await context.Users
                                              .Where(u => u.Email.Equals(dto.Email))
                                              .FirstOrDefaultAsync();

            if (existingEmail != null)
            {
                string errorMessage = $"A voter with the the email '{dto.PhoneNumber}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var passwordEncrypt = PasswordHasher.EncryptPassword(dto.Password);

            user.Occupation = dto.Occupation;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.Password = passwordEncrypt;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
            response.Result = user;
        }
        catch (Exception e) 
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> StepThreeRegister(Guid id, StepThreeRegisterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

        try
        {
            User user = await context.Users
                                     .Where(u => u.Id.Equals(id))
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "User's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            user.PersonalDocumentId = dto.PersonalDocumentId;
            user.HasAgreedToTerms = dto.HasAgreedToTerms;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
            response.Result = user;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> SubStepThreeRegister(Guid id, SubStepThreeRegisterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

        try
        {
            User user = await context.Users
                                      .Where(u => u.Id.Equals(id))
                                      .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "Voter's ID not found.";
                response.ErrorMessage = errorMessage;
                throw new KeyNotFoundException(errorMessage);
            }

            var existingIdNumber = await context.Users
                                                .Where(pd => pd.PIDNumber.Equals(dto.PIDNumber))
                                                .FirstOrDefaultAsync();

            if (existingIdNumber != null)
            {
                string errorMessage = $"Id Number with '{dto.PIDNumber}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var identificationImagePath = await new ImagePathConfig().SaveIdentificationImages(dto.PImage);
            
            user.PIDNumber = dto.PIDNumber;
            user.PImage = identificationImagePath;
            user.IsValidate = false;
            user.IsActive = true;
            user.VerificationStatus = VerifyStatus.Pending;
            user.Role = UserRole.Voter;
            user.DateCreated = DateTime.Now;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
            response.UserRole = UserRole.Voter;
            response.Result = user;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> Login(LoginVoterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
                response.ResponseCode = 400;
                throw new ArgumentException(errorMessage);
            }

            if (voter == null)
            {
                string errorMessage = "Voter not yet registered.";
                response.ErrorMessage = errorMessage;
                response.ResponseCode = 400;
                throw new KeyNotFoundException(errorMessage);
            }

            if (!voter.IsValidate)
            {
                string errorMessage = "Voter registration is pending validation.";
                response.ErrorMessage = errorMessage;
                response.ResponseCode = 403;
                throw new InvalidOperationException(errorMessage);
            }

            if (!voter.IsActive)
            {
                string errorMessage = "Account has been deactivated.";
                response.ErrorMessage = errorMessage;
                response.ResponseCode = 403;
                throw new InvalidOperationException(errorMessage);
            }

            if (!PasswordHasher.VerifyPassword(dto.Password, voter.Password))
            {
                string errorMessage = "Incorrect password entered.";
                response.ErrorMessage = errorMessage;
                response.ResponseCode = 401;
                throw new InvalidOperationException(errorMessage);
            }

            if (string.IsNullOrEmpty(voter.Token))
            {
                var token = Tokens.GenerateTokenV2(dto, voter.Role, configuration);

                if (!string.IsNullOrEmpty(token))
                {
                    voter.Token = token;
                    response.Token = token;
                }
                else
                {
                    string errorMessage = "Failed to generate authentication token.";
                    response.ErrorMessage = errorMessage;
                    response.ResponseCode = 500;
                    throw new InvalidOperationException(errorMessage);
                }
            }

            response.ResponseCode = 200;
            response.UserRole = GetUserRole(voter.VoterId);
            response.Result = voter;
            response.UserId = voter.Id;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public UserRole GetUserRole(int userId)
    {
        var role =  context.Users
                           .Where(u => u.VoterId == userId)
                           .Select(u => u.Role) 
                           .FirstOrDefault();

        if (role == null)
        {
            throw new KeyNotFoundException("User role not found.");
        }

        return role;
    }

    public async Task<bool> GetreCaptchaResponse(string userResponse)
    {
        var reCaptchaSecretKey = configuration["reCaptcha:SecretKey"];

        if (reCaptchaSecretKey != null && userResponse != null)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"secret", reCaptchaSecretKey },
            {"response", userResponse }
        });
            var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<reCaptchaResponse>();
                return result.Success;
            }
        }
        return false;
    }

    public async Task<ApiResponse<User>> Validate(int id)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            voter.VerificationStatus = VerifyStatus.Approved;

            context.Users.Update(voter);
            await context.SaveChangesAsync();
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> Deactivated(int id)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> Activated(int id)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> UpdateVoterProfile(int id, UpdateVoterDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> ForgotPassword(string email)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> ResetPassword(ResetPasswordDto dto)
    {
        ApiResponse<User> response = new ApiResponse<User>();

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
            response.ResponseCode = 500;
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
           throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<ApiResponse<User>> Delete(Guid id)
    {
        ApiResponse<User> response = new ApiResponse<User>();

        try
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) 
            {
                string errorMessage = "No Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();   
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<User>> RememberMe(Guid id)
    {
        ApiResponse<User> response = new ApiResponse<User>();

        try
        {
            var user = await context.Users
                                      .Where(u => u.Id == id)
                                      .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "No Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            user.isRemember = true;
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
}
