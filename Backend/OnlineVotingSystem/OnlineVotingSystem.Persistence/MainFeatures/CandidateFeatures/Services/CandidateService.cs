using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Application.ImageDirectory;
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

    public async Task<ApiResponse<Candidate>> Create(CreateCandidateDto dto)
    {
        ApiResponse<Candidate> response = new ApiResponse<Candidate>();

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

            var candidateImagePath = await new ImagePathConfig().SaveCandidateImages(dto.Image);

            var candidate = mapper.Map<Candidate>(dto);
            candidate.Image = candidateImagePath;
            candidate.DateCreated = DateTime.Now;
            candidate.Status = Domain.Enum.CandidateStatus.Active;

            context.Candidates.Add(candidate);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;

            response.Result = candidate;

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
                        .AsNoTracking()
                        .Include(c => c.PartyAffiliation)
                        .Include(c => c.Votes)
                        .OrderByDescending(c => c.DateCreated)
                        .ToListAsync();

    public async Task<Candidate> GetById(Guid id)
        => await context.Candidates
                        .Where(c => c.Id == id)
                        .FirstOrDefaultAsync();
    
    public async Task<int> GetCountVotes(Guid id)
    {
        var candidateExists = await context.Candidates.AnyAsync(c => c.Id == id);

        if (!candidateExists)
        {
            throw new InvalidOperationException("Candidate Not Found");
        }

        int countVotes = await context.Votes.CountAsync(v => v.CandidateId == id);

        return countVotes;
    }

    public async Task<ApiResponse<Candidate>> Update(Guid id, UpdateCandidateDto dto)
    {
        ApiResponse<Candidate> response = new ApiResponse<Candidate>();

        try
        {
            var candidate = await context.Candidates
                                         .Where(c => c.Id == id)
                                         .FirstOrDefaultAsync();

            if (candidate == null)
            {
                string errorMessage = $"No Candidate Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                candidate.FirstName = dto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                candidate.LastName = dto.LastName;
            }

            if(dto.Image != null)
            {
                var imagePath = await new ImagePathConfig().SaveCandidateImages(dto.Image);
                candidate.Image = imagePath;
            }

            if(!string.IsNullOrWhiteSpace(dto.Gender))
            {
                candidate.Gender = dto.Gender;
            }

            if (dto.PartyAffiliationId != Guid.Empty)
            {
                var partyExist = await context.PartyAffiliations.FindAsync(dto.PartyAffiliationId);

                if (partyExist == null)
                {
                    candidate.PartyAffiliationId = Guid.Empty;
                }
                else
                {
                    candidate.PartyAffiliationId = dto.PartyAffiliationId;
                }
            }

            if (dto.BallotId != Guid.Empty)
            {
                var existBallot = await context.Ballots.FindAsync(dto.BallotId);

                if (existBallot == null)
                {
                    candidate.BallotId = Guid.Empty;
                }
                else
                {
                    candidate.BallotId = dto.BallotId;
                }
            }

            if (dto.PositionId != Guid.Empty) 
            { 
                var existPosition = await context.Positions.FindAsync(dto.PositionId);

                if (existPosition == null)
                {
                    candidate.PositionId = Guid.Empty;
                }
                else
                {
                    candidate.PositionId = dto.PositionId;
                }
            }

            mapper.Map(dto, candidate);
            candidate.DateUpdated = DateTime.Now;
            context.Candidates.Update(candidate);
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

    public async Task<ApiResponse<Candidate>> Delete(Guid id)
    {
        ApiResponse<Candidate> response = new ApiResponse<Candidate>();

        try
        {
            var candidate = await context.Candidates.Where(c => c.Id == id)
                                                    .FirstOrDefaultAsync();

            if (candidate == null)
            {
                string errorMessage = $"No Candidate Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            context.Candidates.Remove(candidate);
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

    public async Task<ApiResponse<Candidate>> ActivateCandidate(Guid id)
    {
        ApiResponse<Candidate> response = new ApiResponse<Candidate>();

        try
        {
            var candidate = await context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                string errorMessage = $"No Candidate Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            candidate.Status = Domain.Enum.CandidateStatus.Active;
            context.Candidates.Update(candidate);
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

    public async Task<ApiResponse<Candidate>> DeactivateCandidate(Guid id)
    {
        ApiResponse<Candidate> response = new ApiResponse<Candidate>();

        try
        {
            var candidate = await context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                string errorMessage = $"No Candidate Id Found.";
                response.ErrorMessage = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }

            candidate.Status = Domain.Enum.CandidateStatus.InActive;
            context.Candidates.Update(candidate);
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
