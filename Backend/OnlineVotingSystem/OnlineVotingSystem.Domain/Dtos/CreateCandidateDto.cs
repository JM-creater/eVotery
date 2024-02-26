using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace OnlineVotingSystem.Domain.Dtos;

public class CreateCandidateDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public Guid BallotId { get; set; }
    [Required]
    public Guid PositionId { get; set; }
}
