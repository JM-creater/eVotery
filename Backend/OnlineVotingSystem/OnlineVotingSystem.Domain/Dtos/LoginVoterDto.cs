using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record LoginVoterDto
{
    public int? VoterId { get; set; }
    public string Email { get; set; } 
    [Required]
    public string Password { get; set; }
    public string RecaptchaToken { get; set; }
}
