using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Entity;

public class Candidate : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string LastName { get; set; } = string.Empty;

    public Guid BallotId { get; set; }
    public virtual Ballot Ballot { get; set; }
    public Guid PositionId { get; set; }
    public virtual Position Position { get; set; }

    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
