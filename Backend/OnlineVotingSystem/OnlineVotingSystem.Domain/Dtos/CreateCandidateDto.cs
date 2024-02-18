using OnlineVotingSystem.Domain.Entity;
namespace OnlineVotingSystem.Domain.Dtos;

public class CreateCandidateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid BallotId { get; set; }
    public Guid PositionId { get; set; }
}
