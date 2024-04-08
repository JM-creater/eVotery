using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record UpdateBallotDto
{
    [Required]
    public string BallotName { get; set; }
    [Required]
    public Guid ElectionId { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
}
