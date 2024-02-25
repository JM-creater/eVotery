namespace OnlineVotingSystem.Domain.Dtos;

public class CreateElectionDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
