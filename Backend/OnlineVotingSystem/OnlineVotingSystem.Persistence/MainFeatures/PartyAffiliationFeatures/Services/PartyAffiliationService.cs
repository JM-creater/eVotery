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

    public async Task<ApiResponse<PartyAffiliation>> Create(CreatePartyAffiliationDto dto)
    {
        ApiResponse<PartyAffiliation> response = new ApiResponse<PartyAffiliation>();

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
            response.Result = party;
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
                        .AsNoTracking()
                        .OrderByDescending(pa => pa.DateCreated)
                        .ToListAsync();

    public async Task<PartyAffiliation> GetById(Guid id)
        => await context.PartyAffiliations
                        .Where(pa => pa.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<int> GetCountPartyMembers(Guid id)
    {
        var party = await context.PartyAffiliations.Include(pa => pa.Candidates)
                                                   .Where(pa => pa.Id == id)
                                                   .SelectMany(pa => pa.Candidates)
                                                   .CountAsync();

        return party;
    }

    public async Task<ApiResponse<PartyAffiliation>> Update(Guid id, UpdatePartyAffiliationDto dto)
    {
        ApiResponse<PartyAffiliation> response = new ApiResponse<PartyAffiliation>();

        try
        {
            var party = await context.PartyAffiliations
                                     .Where(pa => pa.Id == id)
                                     .FirstOrDefaultAsync();

            if (party == null)
            {
                string errorMessage = $"No Party Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            if (!string.IsNullOrWhiteSpace(dto.PartyName))
            {
                party.PartyName = dto.PartyName;
            }

            if (dto.LogoImage != null)
            {
                var imagePath = await new ImagePathConfig().SaveLogoImages(dto.LogoImage);
                party.LogoImage = imagePath;
            }

            mapper.Map(dto, party);
            party.DateUpdated = DateTime.Now;
            context.PartyAffiliations.Update(party);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;

            response.UserRole = null;
        }
        catch (Exception e)
        {
            response.ResponseCode = 400;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<ApiResponse<PartyAffiliation>> Delete(Guid id)
    {
        ApiResponse<PartyAffiliation> response = new ApiResponse<PartyAffiliation>();

        try
        {
            var party = await context.PartyAffiliations
                                        .Where(e => e.Id == id)
                                        .FirstOrDefaultAsync();

            if (party == null)
            {
                string errorMessage = $"No Party Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            context.PartyAffiliations.Remove(party);
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
}
