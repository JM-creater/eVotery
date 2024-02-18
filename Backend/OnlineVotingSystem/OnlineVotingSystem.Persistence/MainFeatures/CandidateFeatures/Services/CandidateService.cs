using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.Services;

public class CandidateService : ICandidateService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    public CandidateService(DataContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    public async Task<ApiResponse> Create(CreateCandidateDto dto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            var existingFirstName = await context.Candidates
                                                 .Where(c => c.FirstName == dto.FirstName)
                                                 .FirstOrDefaultAsync();

            if (existingFirstName != null)
            {
                string errorMessage = $"A candidate with the first name '{dto.FirstName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var existingLastName = await context.Candidates
                                                .Where(c => c.LastName == dto.LastName) 
                                                .FirstOrDefaultAsync();

            if (existingLastName != null)
            {
                string errorMessage = $"A candidate with the last name '{dto.FirstName}' already exists.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            var candidate = mapper.Map<Candidate>(dto);
            candidate.DateCreated = DateTime.Now;
            context.Candidates.Add(candidate);
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

    public async Task<List<Candidate>> GetAll()
        => await context.Candidates
                        .OrderByDescending(c => c.DateCreated)
                        .ToListAsync();

    public async Task<Candidate> GetById(Guid id)
        => await context.Candidates
                        .Where(c => c.Id == id)
                        .FirstOrDefaultAsync(); 
}
