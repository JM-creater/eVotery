using OnlineVotingSystem.Domain.Common;
using OnlineVotingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.Domain.Entity;

public class Voter : BaseEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VoterId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    [Column(TypeName = "nvarchar(30)")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(12)")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string VoterImages { get; set; } = string.Empty;
    public bool IsValidate { get; set; }
    public bool IsActive { get; set; }
    public VerifyStatus VerificationStatus { get; set; } 
    public UserRole Role { get; set; }
}
