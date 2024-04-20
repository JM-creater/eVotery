namespace OnlineVotingSystem.Domain.Dtos;

public record ElectionResultDto
{
    public Guid CandidateId { get; set; }
    public string CandidateName { get; set; }
    public int VoteCount { get; set; }
    public string CandidateImage { get; set; }
    public string Position { get; set; }
}
