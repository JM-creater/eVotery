using OnlineVotingSystem.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.Domain.Entity;

public class PersonalDocument : BaseEntity
{
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Document { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
