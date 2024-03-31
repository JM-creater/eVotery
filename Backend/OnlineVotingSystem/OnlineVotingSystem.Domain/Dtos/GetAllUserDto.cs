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

    //public bool ShouldSerializePersonalDocumentId() => PersonalDocumentId != null;
    //public bool ShouldSerializeFirstName() => !string.IsNullOrEmpty(FirstName);
    //public bool ShouldSerializeLastName() => !string.IsNullOrEmpty(LastName);
    //public bool ShouldSerializeSuffixName() => !string.IsNullOrEmpty(SuffixName);
    //public bool ShouldSerializeDateOfBirth() => DateOfBirth != null;
    //public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);
    //public bool ShouldSerializePassword() => !string.IsNullOrEmpty(Password);
    //public bool ShouldSerializeAddress() => !string.IsNullOrEmpty(Address);
    //public bool ShouldSerializePhoneNumber() => !string.IsNullOrEmpty(PhoneNumber);
    //public bool ShouldSerializeGender() => !string.IsNullOrEmpty(Gender);
    //public bool ShouldSerializeNationality() => !string.IsNullOrEmpty(Nationality);
    //public bool ShouldSerializeReligion() => !string.IsNullOrEmpty(Religion);
    //public bool ShouldSerializeZipCode() => !string.IsNullOrEmpty(ZipCode);
    //public bool ShouldSerializeOccupation() => !string.IsNullOrEmpty(Occupation);
    //public bool ShouldSerializeVoterImages() => !string.IsNullOrEmpty(VoterImages);
}
