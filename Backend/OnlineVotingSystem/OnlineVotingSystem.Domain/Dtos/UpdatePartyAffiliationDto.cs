using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineVotingSystem.Domain.Dtos;

public record UpdatePartyAffiliationDto
{
    [Required]
    public string PartyName { get; set; }
    [Required]
    public IFormFile LogoImage { get; set; }
}
