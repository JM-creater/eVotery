﻿using OnlineVotingSystem.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class LoginVoterDto
{
    [Required]
    public int? VoterId { get; set; }
    [Required]
    public string Email { get; set; } 
    [Required]
    public string Password { get; set; }
}
