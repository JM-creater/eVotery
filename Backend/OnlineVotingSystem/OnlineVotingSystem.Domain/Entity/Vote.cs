using OnlineVotingSystem.Domain.Common;

namespace OnlineVotingSystem.Domain.Entity;

public class Vote : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User Voter { get; set; }

    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }

    public DateTime VotedAt { get; set; }
}
