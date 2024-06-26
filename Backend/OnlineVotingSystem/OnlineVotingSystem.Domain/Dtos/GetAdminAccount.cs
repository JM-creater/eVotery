﻿using Microsoft.AspNetCore.Http;
using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Dtos;

public record GetAdminAccount
{
    public int VoterId { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } 
    public string Address { get; set; } 
    public string PhoneNumber { get; set; }
    public string VoterImages { get; set; }
    public bool IsValidate { get; set; }
    public bool IsActive { get; set; }
    public VerifyStatus VerificationStatus { get; set; }
    public UserRole Role { get; set; }

}
