using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class UpdateElectionDto
{
    [Required]
    public string ElectionName { get; set; }

    [Required]
    public DateTime? StartDate { get; set; }

    [Required]
    public DateTime? EndDate { get; set; }
}
