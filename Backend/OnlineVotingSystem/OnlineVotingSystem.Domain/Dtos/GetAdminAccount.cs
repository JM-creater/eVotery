using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Dtos;

public class GetAdminAccount
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } 
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public bool IsValidate { get; set; }
    public bool IsActive { get; set; }
    public VerifyStatus VerificationStatus { get; set; }
    public UserRole Role { get; set; }

}
