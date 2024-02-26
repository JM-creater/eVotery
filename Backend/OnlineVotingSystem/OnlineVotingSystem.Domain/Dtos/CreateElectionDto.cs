using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class CreateElectionDto
{
    [Required]
    public string ElectionName { get; set; } 
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
