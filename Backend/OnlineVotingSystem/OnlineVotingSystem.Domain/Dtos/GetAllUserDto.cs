namespace OnlineVotingSystem.Domain.Dtos;

public class GetAllUserDto
{
    public int VoterId { get; set; }
    public Guid? PersonalDocumentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SuffixName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? Religion { get; set; }
    public string? ZipCode { get; set; }
    public string? Occupation { get; set; }
    public string? VoterImages { get; set; }
    public bool HasAgreedToTerms { get; set; }
}
