using AutoMapper;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.Helpers.MapperProfile;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Voter
        CreateMap<CreateVoterDto, User>();
        CreateMap<UpdateVoterDto, User>()
            .ForMember(dest => dest.VoterImages, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        // Candidate
        CreateMap<CreateCandidateDto, Candidate>();

        // Ballot
        CreateMap<CreateBallotDto, Ballot>();

        // Election
        CreateMap<CreateElectionDto, Election>();
    }
}
