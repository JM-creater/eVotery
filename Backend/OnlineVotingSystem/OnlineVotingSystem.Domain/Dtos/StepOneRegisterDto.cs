using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.Domain.Dtos;

public record StepOneRegisterDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string SuffixName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Nationality { get; set; }
    [Required]
    public string Religion { get; set; }
    [Required]
    public string ZipCode { get; set; }
}
