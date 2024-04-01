using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public class SubStepThreeRegisterDto
{
    [Required]
    public string IdNUmber { get; set; }
    [Required]
    public IFormFile Image { get; set; }
}
