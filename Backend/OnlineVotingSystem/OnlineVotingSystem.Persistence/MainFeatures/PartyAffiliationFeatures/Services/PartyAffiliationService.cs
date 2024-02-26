using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Application.ImageDirectory;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.Services;

public class PartyAffiliationService : IPartyAffiliationService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    public PartyAffiliationService(DataContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    public async Task<ApiResponse> Create(CreatePartyAffiliationDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            var existingName = await context.PartyAffiliations
                                            .Where(pa => pa.PartyName == dto.PartyName)
                                            .FirstOrDefaultAsync();

            if (existingName != null)
            {
                string errorMessage = $"A party affiliation with the name '{dto.PartyName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var logoImagePath = await new ImagePathConfig().SaveLogoImages(dto.LogoImage);

            var party = mapper.Map<PartyAffiliation>(dto);
            party.DateCreated = DateTime.Now;
            party.IsActive = true;
            party.LogoImage = logoImagePath;

            context.PartyAffiliations.Add(party);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<List<PartyAffiliation>> GetAll()
        => await context.PartyAffiliations
                        .OrderByDescending(pa => pa.DateCreated)
                        .ToListAsync();

    public async Task<PartyAffiliation> GetById(Guid id)
        => await context.PartyAffiliations
                        .Where(pa => pa.Id == id)
                        .FirstOrDefaultAsync();

}
