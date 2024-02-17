using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Entity;

public class Ballot : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public bool IsActive { get; set; }
    public Guid ElectionId { get; set; }
    public virtual Election Election { get; set; }

    public ICollection<Ballot> Ballots { get; set; } = new List<Ballot>();
    public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
