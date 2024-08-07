﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace OnlineVotingSystem.Domain.Dtos;

public record CreateCandidateDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public Guid BallotId { get; set; }
    [Required]
    public Guid PositionId { get; set; }
    [Required]
    public Guid PartyAffiliationId { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string Biography { get; set; }
}
