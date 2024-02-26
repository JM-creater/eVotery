using OnlineVotingSystem.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class CreateBallotDto
{
    [Required]
    public string Name { get; set; } 
    [Required]
    public Guid ElectionId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
