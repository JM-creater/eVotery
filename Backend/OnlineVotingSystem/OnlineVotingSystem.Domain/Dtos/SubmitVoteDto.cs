using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record SubmitVoteDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CandidateId { get; set; }
}
