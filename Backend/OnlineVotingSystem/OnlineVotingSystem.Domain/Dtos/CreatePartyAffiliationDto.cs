using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record CreatePartyAffiliationDto
{
    [Required]
    public string PartyName { get; set; } = string.Empty;
    [Required]
    public IFormFile LogoImage { get; set; }
}
