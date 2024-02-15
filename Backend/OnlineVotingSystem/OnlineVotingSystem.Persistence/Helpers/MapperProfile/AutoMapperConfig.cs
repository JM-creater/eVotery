using AutoMapper;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.Helpers.MapperProfile;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<CreateVoterDto, Voter>();
        CreateMap<UpdateVoterDto, Voter>()
            .ForMember(dest => dest.VoterImages, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }
}
