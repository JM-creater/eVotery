using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineVotingSystem.Domain.Enum;

namespace OnlineVotingSystem.Domain.Entity;

public class Candidate : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string FirstName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string LastName { get; set; } 

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string Image { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(10)")]
    public string Gender { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string Biography { get; set; }

    public Guid PartyAffiliationId { get; set; }
    public virtual PartyAffiliation PartyAffiliation { get; set; }

    public Guid BallotId { get; set; }
    public virtual Ballot Ballot { get; set; }

    public Guid PositionId { get; set; }
    public virtual Position Position { get; set; }

    public CandidateStatus Status { get; set; }

    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
