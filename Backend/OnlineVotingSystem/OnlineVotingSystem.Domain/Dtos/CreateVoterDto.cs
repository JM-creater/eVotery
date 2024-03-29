using Microsoft.AspNetCore.Http;
using OnlineVotingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class CreateVoterDto
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string SuffixName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public string Address { get; set; }
    public string PhoneNumber { get; set; } 
    public string Gender { get; set; }
    public IFormFile VoterImages { get; set; }
    public bool HasAgreedToTerms { get; set; }
}
