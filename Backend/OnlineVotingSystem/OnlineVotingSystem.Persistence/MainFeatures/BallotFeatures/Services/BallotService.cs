﻿using AutoMapper;
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

    public async Task<ApiResponse> Create(CreateBallotDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            var existingName = await context.Ballots
                                            .Where(e => e.Name == dto.Name)
                                            .FirstOrDefaultAsync();

            if (existingName != null)
            {
                string errorMessage = $"A election with the name '{dto.Name}' already exists.";
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
}
