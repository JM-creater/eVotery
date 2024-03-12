using Microsoft.AspNetCore.Http;
using OnlineVotingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class CreateVoterDto
{
    [Required]
    public string FirstName { get; set; } 
    [Required]
    public string LastName { get; set; } 
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; } 
    [Required]
    public string Address { get; set; }
    [Required]
    public string PhoneNumber { get; set; } 
    [Required]
    public string Gender { get; set; }
    [Required]
    public IFormFile VoterImages { get; set; }
}
