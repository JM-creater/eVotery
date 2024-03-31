using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.Domain.Entity;

public class Position : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } 
    public bool IsActive { get; set; }

    public ICollection<Candidate> Candidates { get; set;} = new List<Candidate>();
}
