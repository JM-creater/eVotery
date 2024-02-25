using OnlineVotingSystem.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class CreateBallotDto
{
    public string Name { get; set; } = string.Empty;
    public Guid ElectionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
