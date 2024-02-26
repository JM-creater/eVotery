using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.Domain.Entity;

public class PartyAffiliation : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string PartyName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string LogoImage { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
