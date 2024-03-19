using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.Services;

public class BallotService : IBallotService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    public BallotService(DataContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    public async Task<ApiResponse<Ballot>> Create(CreateBallotDto dto)
    {
        ApiResponse<Ballot> response = new ApiResponse<Ballot>();

        try
        {
            var existingName = await context.Ballots
                                            .Where(e => e.BallotName == dto.BallotName)
                                            .FirstOrDefaultAsync();

            if (existingName != null)
            {
                string errorMessage = $"A election with the name '{dto.BallotName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var ballot = mapper.Map<Ballot>(dto);
            ballot.IsActive = true;
            ballot.DateCreated = DateTime.Now;

            context.Ballots.Add(ballot);
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

    public async Task<List<Ballot>> GetAll()
        => await context.Ballots
                        .OrderByDescending(b => b.DateCreated)
                        .ToListAsync();

    public async Task<Ballot> GetById(Guid id)
        => await context.Ballots
                        .Where(b => b.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<ApiResponse<Ballot>> Update(Guid id, UpdateBallotDto dto)
    {
        ApiResponse<Ballot> response = new ApiResponse<Ballot>();

        try
        {
            var ballot = await context.Ballots
                                      .Where(b => b.Id == id)
                                      .FirstOrDefaultAsync();

            if (ballot == null)
            {
                string errorMessage = $"No Ballot Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            if (!string.IsNullOrWhiteSpace(dto.BallotName))
            {
                ballot.BallotName = dto.BallotName;
            }

            if (dto.ElectionId != Guid.Empty)
            {
                var electionExist = await context.Elections.FindAsync(dto.ElectionId);

                if (electionExist == null)
                {
                    ballot.ElectionId = Guid.Empty;
                }
                else
                {
                   ballot.ElectionId = dto.ElectionId;
                }
            }

            if (dto.StartDate.HasValue)
            {
                ballot.StartDate = dto.StartDate.Value;
            }

            if (dto.EndDate.HasValue)
            {
                ballot.EndDate = dto.EndDate.Value;
            }

            mapper.Map(dto, ballot);
            ballot.DateUpdated = DateTime.Now;
            context.Ballots.Update(ballot);
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

    public async Task<ApiResponse<Ballot>> Delete(Guid id)
    {
        ApiResponse<Ballot> response = new ApiResponse<Ballot>();

        try
        {
            var ballot = await context.Ballots
                                      .Where(b => b.Id == id)
                                      .FirstOrDefaultAsync();    
            
            if (ballot == null)
            {
                string errorMessage = $"No Ballot Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            context.Ballots.Remove(ballot);
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
