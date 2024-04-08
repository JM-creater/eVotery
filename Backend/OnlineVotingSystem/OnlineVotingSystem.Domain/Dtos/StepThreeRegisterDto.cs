using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record StepThreeRegisterDto
{
    [Required]
    public Guid PersonalDocumentId { get; set; }
    [Required]
    public bool HasAgreedToTerms { get; set; }
}
