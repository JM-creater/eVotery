using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Entity;

public class Ballot : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string BallotName { get; set; }

    public Guid ElectionId { get; set; }
    public virtual Election Election { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
