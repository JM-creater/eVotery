using Microsoft.AspNetCore.Http;
using OnlineVotingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;
namespace OnlineVotingSystem.Domain.Dtos;

public class CreateCandidateDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public Guid BallotId { get; set; }
    [Required]
    public Guid PositionId { get; set; }
    [Required]
    public Guid PartyAffiliationId { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string Biography { get; set; }
}
