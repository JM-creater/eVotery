using OnlineVotingSystem.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class SubmitVoteDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CandidateId { get; set; }
}
