﻿using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.Services;

public class PositionService : IPositionService
{
    private readonly DataContext context;
    public PositionService(DataContext _context)
    {
        context = _context;
    }

    public async Task<List<Position>> GetAll()
        => await context.Positions
                        .AsNoTracking()
                        .Include(p => p.Candidates)
                            .ThenInclude(c => c.Votes)
                        .ToListAsync();

    public async Task<Position> GetById(Guid id)
        => await context.Positions
                        .Where(p => p.Id == id)
                        .FirstOrDefaultAsync(); 

    public async Task<Dictionary<string, int>> GetNumberOfCandidatePosition()
    {
        var position = await context.Positions
                                    .Include(p => p.Candidates)
                                    .ToListAsync();

        var counts = position.ToDictionary(
            p => p.Name,
            p => p.Candidates.Count()
        );

        return counts;
    }

    public async Task<ApiResponse<Position>> Create(string name)
    {
        ApiResponse<Position> response = new ApiResponse<Position>();

        try
        {
            var existngPositionName = await context.Positions
                                                   .Where(p => p.Name == name)
                                                   .FirstOrDefaultAsync();

            if (existngPositionName != null) 
            {
                string errorMessage = $"A position with '{name}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var position = new Position
            {
                Name = name
            };

            position.IsActive = true;

            context.Positions.Add(position);
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

    public async Task<ApiResponse<Position>> Update(Guid id, UpdatePositionDto dto)
    {
        ApiResponse<Position> response = new ApiResponse<Position>();

        try
        {
            var existingPosition = await context.Positions.FindAsync(id);

            if (existingPosition == null)
            {
                string errorMessage = $"Position not found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var anotherPositionWithSameName = await context.Positions
                                                           .AnyAsync(p => p.Name == dto.Name && 
                                                                          p.Id != id);

            if (anotherPositionWithSameName)
            {
                response.ErrorMessage = $"A position with name '{dto.Name}' already exists.";
                response.ResponseCode = 400;
                return response;
            }

            existingPosition.Name = dto.Name;

            context.Positions.Update(existingPosition);
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

    public async Task<ApiResponse<Position>> Delete(Guid id)
    {
        ApiResponse<Position> response = new ApiResponse<Position>();

        try
        {
            var position = await context.Positions
                                         .Where(p => p.Id == id)
                                         .FirstOrDefaultAsync();

            if (position == null)
            {
                string errorMessage = $"Id is not found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            context.Positions.Remove(position);
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
