﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record SubStepThreeRegisterDto
{
    [Required]
    public string PIDNumber { get; set; }
    [Required]
    public IFormFile PImage { get; set; }
}
