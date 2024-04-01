using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class StepThreeRegisterDto
{
    [Required]
    public Guid PersonalDocumentId { get; set; }
    [Required]
    public bool HasAgreedToTerms { get; set; }
}
