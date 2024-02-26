using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.Services;

public class ElectionService : IElectionService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    public ElectionService(DataContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;   
    }

    public async Task<ApiResponse> Create(CreateElectionDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            var existingName = await context.Elections
                                            .Where(e => e.ElectionName == dto.ElectionName)
                                            .FirstOrDefaultAsync();

            if (existingName != null) 
            {
                string errorMessage = $"A election with the name '{dto.ElectionName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var election = mapper.Map<Election>(dto);
            election.DateCreated = DateTime.Now;
            election.IsActive = true;

            context.Elections.Add(election);
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

    public async Task<List<Election>> GetAll()
        => await context.Elections
                        .OrderByDescending(e => e.DateCreated)
                        .ToListAsync();

    public async Task<Election> GetById(Guid id)
        => await context.Elections
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
}
