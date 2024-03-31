using AutoMapper;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.Helpers.MapperProfile;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Voter
        CreateMap<User, GetAllUserDto>();
            //.ForMember(dest => dest.PersonalDocumentId, opt => opt.MapFrom(src => src.PersonalDocumentId ?? Guid.Empty))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
            //.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password ?? string.Empty))
            //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            //.ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation ?? string.Empty))
            //.ForMember(dest => dest.VoterImages, opt => opt.MapFrom(src => src.VoterImages ?? string.Empty));
        CreateMap<CreateVoterDto, User>();
        CreateMap<StepOneRegisterDto, User>();
        CreateMap<UpdateVoterDto, User>()
            .ForMember(dest => dest.VoterImages, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        // Candidate
        CreateMap<CreateCandidateDto, Candidate>();
        CreateMap<UpdateCandidateDto, Candidate>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());

        // Ballot
        CreateMap<CreateBallotDto, Ballot>();
        CreateMap<UpdateBallotDto, Ballot>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Election
        CreateMap<CreateElectionDto, Election>();
        CreateMap<UpdateElectionDto, Election>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Party Affiliation
        CreateMap<CreatePartyAffiliationDto, PartyAffiliation>();   
        CreateMap<UpdatePartyAffiliationDto, PartyAffiliation>()
            .ForMember(dest => dest.LogoImage, opt => opt.Ignore());
    }
}
