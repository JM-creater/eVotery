﻿using AutoMapper;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.Helpers.MapperProfile;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Voter
        CreateMap<CreateVoterDto, User>();
        CreateMap<StepOneRegisterDto, User>();
        CreateMap<StepTwoRegisterDto, User>();
        CreateMap<StepThreeRegisterDto, User>();
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

        // Personal Document
        CreateMap<SubStepThreeRegisterDto, PersonalDocument>();

        // Votes
        CreateMap<SubmitVoteDto, Vote>();
    }   
}
