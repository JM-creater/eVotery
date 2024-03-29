﻿using OnlineVotingSystem.Domain.Common;
using OnlineVotingSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.Domain.Entity;

public class User : BaseEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VoterId { get; set; }

    [Required]
    public Guid? PersonalDocumentId { get; set; }
    public virtual PersonalDocument PersonalDocument { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string? FirstName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string? LastName { get; set; }

    [Column(TypeName = "nvarchar(4)")]
    public string? SuffixName { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    [Column(TypeName = "nvarchar(30)")]
    public string? Email { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Password { get; set; } 

    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string? Address { get; set; } 

    [Required]
    [Column(TypeName = "nvarchar(12)")]
    public string? PhoneNumber { get; set; } 

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string? Gender { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string? Nationality { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string? Religion { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(4)")]
    public string ZipCode { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Occupation { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string? VoterImages { get; set; } = string.Empty;

    [Required]
    public bool HasAgreedToTerms { get; set; }

    public bool IsValidate { get; set; }
    public bool IsActive { get; set; }
    public VerifyStatus VerificationStatus { get; set; } 
    public UserRole Role { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }

    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
